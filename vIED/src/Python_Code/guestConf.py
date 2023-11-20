#!/root/Virtual-PAC/vIED/vEnv/bin/python3

import subprocess, re, yaml, psutil

def hostUpdate():
        
    def getIp():
        def getIface() -> str:
            for d in psutil.net_if_stats().keys():
                if d != 'lo':
                    return str(d)
            return None
        iface = getIface()
        if iface is None:
            return None
        try:
            with open('/etc/netplan/00-installer-config.yaml', 'r') as file:
                yamlData = yaml.safe_load(file)
                return yamlData['network']['ethernets'][iface]['addresses'][0]
        except:
            return None

    def getVmType():
        try:
            result = subprocess.run(["systemctl", "is-enabled", "vIED.service"], capture_output=True, text=True, check=True)
            output = result.stdout.strip()
            if output == "enabled":
                return 'IED'
        except Exception as ex:
            try:
                result = subprocess.run(["systemctl", "is-enabled", "vMU.service"], capture_output=True, text=True, check=True)
                output = result.stdout.strip()
                if output == "enabled":
                    return 'MU'
            except:
                return None
    try:
        dmidecode_output = subprocess.check_output(["sudo", "dmidecode", "-t", "1"], text=True)
    except subprocess.CalledProcessError:
        print("Error running dmidecode")
        exit(1)

    uuid_pattern = r"UUID:\s+([0-9a-fA-F-]+)"
    match = re.search(uuid_pattern, dmidecode_output)


    if match:
        uuid = match.group(1)
        data = {
            'ip': getIp(),
            'type': getVmType()
        }
        with open(f'/root/host-shm/vmConfig/{uuid}', 'w') as file:
            yaml.safe_dump(data, file)
    else:
        print("UUID not found in dmidecode output")
