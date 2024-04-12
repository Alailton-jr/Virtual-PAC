#!/root/Virtual-PAC/vQualityMeter/vEnv/bin/python3
import numpy as np

def voltageSag(fs:int, frequency:int, phaseAng:float, duration:float, depth:float, start:float, end:float, decayRate:float = None, riseRate:float = None) -> np.ndarray:
    """
    Generate a voltage sag signal.

    Parameters
    ----------
    fs : int
        The sampling frequency.
    frequency : int
        The frequency of the signal.
    phaseAng : float
        The phase angle of the signal in degrees.
    duration : float
        The duration of the signal.
    depth : float
        The depth of the signal.
    start : float
        The start time of the signal.
    end : float
        The end time of the signal.
    decayRate : float [Optional]
        The decay rate of the signal.
    riseRate : float [Optional]
        The rise rate of the signal.
        
    Returns
    -------
    numpy.ndarray
        The generated signal.
    """
    if decayRate is None:
        decayRate = 0
        hasDecayRate = 0
    if riseRate is None:
        riseRate = 0
        hasRiseRate = 0
    
    t = np.linspace(0, duration, int(fs*duration))
    u1 = np.heaviside(t - start, 1)
    u2 = np.heaviside(t - end, 1)
    sag = (1 - depth*(u1*(1- hasDecayRate * np.exp(-decayRate*(t-start))) - u2*(1- hasRiseRate * np.exp(-riseRate*(t-end))) )) * np.sin(2 * np.pi * frequency * t + np.deg2rad(phaseAng))

    return sag

def voltageSwell(fs:int, frequency:int, phaseAng:float, duration:float, depth:float, start:float, end:float, decayRate:float = None, riseRate:float = None) -> np.ndarray:
    """
    Generate a voltage swell signal.

    Parameters
    ----------
    fs : int
        The sampling frequency.
    frequency : int
        The frequency of the signal.
    phaseAng : float
        The phase angle of the signal in degrees.
    duration : float
        The duration of the signal.
    depth : float
        The depth of the signal.
    start : float
        The start time of the signal.
    end : float
        The end time of the signal.
    decayRate : float [Optional]
        The decay rate of the signal.
    riseRate : float [Optional]
        The rise rate of the signal.
        
    Returns
    -------
    numpy.ndarray
        The generated signal.
    """
    if decayRate is None:
        decayRate = 0
        hasDecayRate = 0
    if riseRate is None:
        riseRate = 0
        hasRiseRate = 0
    
    t = np.linspace(0, duration, int(fs*duration))
    u1 = np.heaviside(t - start, 1)
    u2 = np.heaviside(t - end, 1)
    swell = (1 + depth*(u1*(1- hasDecayRate * np.exp(-decayRate*(t-start))) - u2*(1- hasRiseRate * np.exp(-riseRate*(t-end))) )) * np.sin(2 * np.pi * frequency * t + np.deg2rad(phaseAng))

    return swell

def voltageTransient(fs:int, frequency:int, phaseAng:float, duration:float, start:float, magnitude:float, oscilatoryFrequency:float, settingRate:float):
    """
    Generate a voltage transient signal.

    Parameters
    ----------
    fs : int
        The sampling frequency.
    frequency : int
        The frequency of the signal.
    phaseAng : float
        The phase angle of the signal in degrees.
    duration : float
        The duration of the signal.
    start : float
        The start time of the signal.
    magnitude : float
        The magnitude of the signal.
    oscilatoryFrequency : float
        The oscilatory frequency of the signal.
    settingRate : float
        The setting rate of the signal.

    Returns
    -------
    numpy.ndarray
        The generated signal.
    """
    t = np.linspace(0, duration, int(fs*duration))

    u1 = np.heaviside(t - start, 1)
    transient = np.sin(2 * np.pi * frequency * t + np.deg2rad(phaseAng)) + (magnitude * np.sin(2 * np.pi * oscilatoryFrequency * (t-start)) * np.exp(-settingRate * t) * u1)


    return transient


if __name__ == '__main__':
    import matplotlib.pyplot as plt

    voltage = 1
    frequency = 60
    fs = 10e3
    duration = 0.5
    depth = 0.6
    start = 0.1
    end = 0.3
    decayRate = 80
    riseRate = 100
    phaseAng = 0
    sag = voltageSwell(fs, frequency, phaseAng, duration, depth, start, end)
    transient = voltageTransient(
        fs, frequency, phaseAng, duration, start, magnitude=1.9, oscilatoryFrequency=120, settingRate=10
    )

    plt.plot(transient)
    plt.savefig('voltage_sag.png')