
from . import loadYaml, os, fileFolder, SampledValue, QualityAnalyse, QualityEvent, addSampledValue, ctypes, subprocess, getIface, signal, send_signal_to_process, shutil, re, openSvMemory, SampledValueControl, deleteSvShm

netMonitorDir = os.path.join(fileFolder, '..', '..', 'C_Build')
netMonitorSetupDir = os.path.join(fileFolder, '..', '..')
netMonitorSetup = os.path.join(netMonitorSetupDir, 'monitorSetup.yaml')
snifferFile = os.path.join(netMonitorDir, 'qualitySniffer')
analyseFile = os.path.join(netMonitorDir, 'qualityAnalyse')

analyseEventsDir = os.path.join(netMonitorDir, 'files')

snifferCommand = f'{snifferFile} {getIface()}'
analyseCommand = f'{analyseFile}'

svControl = SampledValueControl()

def saveMonitorSetup(data:str):
    try:
        with open(netMonitorSetup, 'w') as file:
            file.write(data)
        return True
    except:
        return False

def addMonitorSampledValue() -> bool:

    if not os.path.exists(netMonitorSetup):
        return False
    try:
        svData = loadYaml(netMonitorSetup)
        i_sv = 0
        for data in svData["SampledValues"]:
            nomVoltage = data["nominalVoltage"]
            nomCurrent = data["nominalCurrent"]
            sv = SampledValue(
                svId = data["SVID"],
                smpRate= data["smpRate"],
                freq= data["frequency"],
                numChannels= data["noChannels"],
                analyseData = QualityAnalyse(
                    sag = QualityEvent(
                        topThreshold = data["sag"]["topThreshold"] * nomVoltage,
                        bottomThreshold = data["sag"]["bottomThreshold"] * nomVoltage,
                        minDuration = data["sag"]["minDuration"],
                        maxDuration = data["sag"]["maxDuration"],
                        eventName = "sag"
                    ),
                    swell = QualityEvent(
                        topThreshold = data["swell"]["topThreshold"] * nomVoltage,
                        bottomThreshold = data["swell"]["bottomThreshold"] * nomVoltage,
                        minDuration = data["swell"]["minDuration"],
                        maxDuration = data["swell"]["maxDuration"],
                        eventName = "swell"
                    ),
                    interruption = QualityEvent(
                        topThreshold = data["interruption"]["topThreshold"] * nomVoltage,
                        bottomThreshold = data["interruption"]["bottomThreshold"] * nomVoltage,
                        minDuration = data["interruption"]["minDuration"],
                        maxDuration = data["interruption"]["maxDuration"],
                        eventName = "interruption"
                    ),
                    overVoltage = QualityEvent(
                        topThreshold = data["overVoltage"]["topThreshold"] * nomVoltage,
                        bottomThreshold = data["overVoltage"]["bottomThreshold"] * nomVoltage,
                        minDuration = data["overVoltage"]["minDuration"],
                        maxDuration = data["overVoltage"]["maxDuration"],
                        eventName = "overVoltage"
                    ),
                    underVoltage = QualityEvent(
                        topThreshold = data["underVoltage"]["topThreshold"] * nomVoltage,
                        bottomThreshold = data["underVoltage"]["bottomThreshold"] * nomVoltage,
                        minDuration = data["underVoltage"]["minDuration"],
                        maxDuration = data["underVoltage"]["maxDuration"],
                        eventName = "underVoltage"
                    )
                )
            )
            svStruct = sv.getCStruct()
            svControl.addSampledValue(svStruct, data["SVID"], i_sv)
            i_sv += 1
        return True
    except Exception as e:
        print("An error occurred:", e)
        return False

#region Run Scripts

def runSniffer():
    subprocess.Popen(snifferCommand, shell=True)

def stopSniffer():
    send_signal_to_process('qualitySniffer', None, signal.SIGTERM)

def restartSniffer():
    stopSniffer()
    runSniffer()

def runAnalyse():
    subprocess.Popen(analyseCommand, shell=True)

def stopAnalyse():
    send_signal_to_process('qualityAnalyse', None, signal.SIGTERM)

def restartAnalyse():
    stopAnalyse()
    runAnalyse()

def runMonitor(sniffer:bool = True, analyser:bool = True):
    stopMonitor()
    if addMonitorSampledValue():
        if os.path.exists(analyseEventsDir):
            shutil.rmtree(analyseEventsDir)
        os.mkdir(analyseEventsDir)
        if analyser:
            runAnalyse()
        if sniffer:
            runSniffer()
        svControl.shmOpenned = False

def stopMonitor(sniffer:bool = True, analyser:bool = True):
    if analyser:
        stopAnalyse()
    if sniffer:
        stopSniffer()
    deleteSvShm()

#endregion

information = {
    "Type": "",
    "Duration": "",
    "minValue": "",
    "maxValue": "",
    "Date": ""
}
def format_date_time(string):
    pattern = r'(\d{4})-(\d{2})-(\d{2})_(\d{2})-(\d{2})-(\d{2})'
    match = re.search(pattern, string)
    if match:
        year, month, day, hour, minute, second = match.groups()
        formatted_date_time = f"{year}/{month}/{day} {hour}:{minute}:{second}"
        return formatted_date_time
    else:
        return None

def deleteLogByName(svId, lineName):
    file_path = os.path.join(analyseEventsDir, svId + '.info')
    if os.path.exists(file_path):
        with open(file_path, 'r') as file:
            lines = file.readlines()
        updated_lines = [line for line in lines if lineName not in line]
        with open(file_path, 'w') as file:
            file.writelines(updated_lines)

def getAnalyseEvents(svId:str):
    events = []
    if os.path.exists(os.path.join(analyseEventsDir, svId + '.info')):
        with open(os.path.join(analyseEventsDir, svId + '.info'), 'r') as file:
            for line in file:
                data = [word.strip() for word in line.split('|')]
                name = data[4].split('/')[-1].split('.')[0]
                date = format_date_time(name)
                events.append({
                    "Type": data[0],
                    "Duration": data[1],
                    "MinValue": data[2],
                    "MaxValue": data[3],
                    "Name": name,
                    "Date": date
                })
        return events
    return None

def getAnalyseWaveForm(name:str):
    for _file in os.listdir(analyseEventsDir):
        if name in _file:
            with open(os.path.join(analyseEventsDir, _file), 'rb') as file:
                return file.read()
    return None

def getAnalyseSvInfo(svId:str):
    sv = svControl.getSvData(svId)
    qualityAnalyse = sv.analyseData
    ret = {
        "Flags":{
            "Sag": qualityAnalyse.sag.flag,
            "Swell": qualityAnalyse.swell.flag,
            "interruption": qualityAnalyse.interruption.flag,
            "OverVoltage": qualityAnalyse.overVoltage.flag,
            "UnderVoltage": qualityAnalyse.underVoltage.flag
        },
        "Rms": qualityAnalyse.rms,
        "Symmetrical": qualityAnalyse.symmetrical,
        "Unbalance": qualityAnalyse.unbalance,
        "Phasor": qualityAnalyse.phasor_polar,
        "Found": sv.found
    }
    return ret

    
    








