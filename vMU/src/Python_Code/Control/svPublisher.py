#!/root/Virtual-PAC/vMU/vEnv/bin/python3

from pyasn1.type import univ, char, namedtype, namedval, tag, constraint
from pyasn1.codec.ber import encoder

class SvPublisher:
    def __init__(self, AppId=0x4000,macDst='01:0c:cd:04:00:00',macSrc='9c:da:3e:84:a1:d4',vLanId=None, vLanPriority=4) -> None:
        
        self.savPdu = SampledValues()['savPdu']
        self.header = bytes()
        self.AppId = AppId
        self.macDst = macDst
        self.macSrc = macSrc
        self.AppId = AppId
        self.vLanId = vLanId
        self.vLanPriority = vLanPriority
        self.setHeader()
    
    def setHeader(self,):
        self.header = bytes()
        self.header += bytes.fromhex(self.macDst.replace(':','').replace('-','')) #macDst
        self.header += bytes.fromhex(self.macSrc.replace(':','').replace('-','')) #macSrc
        if self.vLanId is not None: #vLan
            self.header += bytes.fromhex('8100')
            vLan = self.vLanPriority << 13 | self.vLanId
            self.header += bytes.fromhex('{:04x}'.format(vLan))
        self.header += bytes.fromhex('88ba') #EtherType
        self.header += bytes.fromhex('{:04x}'.format(self.AppId))

    def asduSetup(self, svId, confRev, datSet = None, refrTm = None, smpSynch = None, smpRate = None, smpMod = None, gmidData = None):
        self.svId = svId
        self.datSet = datSet
        self.confRev = confRev
        self.refrTm = refrTm
        self.smpSynch = smpSynch
        self.smpRate = smpRate
        self.smpMod = smpMod
        self.gmidData = gmidData

    # values[asdu][chanel][value, q]
    def getFrame(self, values, smpCount):
        for i in range(len(values)):
            self.savPdu['seqASDU'][i]['svID'] = self.svId
            if self.datSet is not None:
                self.savPdu['seqASDU'][i]['datSet'] = self.datSet
            self.savPdu['seqASDU'][i]['smpCnt'] = bytes(int.to_bytes(smpCount + i, byteorder='big', length=2))
            self.savPdu['seqASDU'][i]['confRev'] = self.confRev
            if self.refrTm is not None:
                self.savPdu['seqASDU'][i]['refrTm'] = self.refrTm
            if self.smpSynch is not None:
                self.savPdu['seqASDU'][i]['smpSynch'] = self.smpSynch
            if self.smpRate is not None:
                self.savPdu['seqASDU'][i]['smpRate'] = self.smpRate
            if self.smpMod is not None:
                self.savPdu['seqASDU'][i]['smpMod'] = self.smpMod
            if self.gmidData is not None:
                self.savPdu['seqASDU'][i]['gmidData'] = self.gmidData
            data = bytes()
            for j in range(len(values[i])):
                data += int.to_bytes(int(values[i][j][0]), byteorder='big', signed=True, length=4)
                data += int.to_bytes(int(values[i][j][1]), byteorder='big', signed=True, length=4)
            self.savPdu['seqASDU'][i]['seqData'] = data
        self.savPdu['noASDU'] = len(values)
        _savPdu = encoder.encode(self.savPdu)
        length = len(_savPdu) + 10
        return self.header + int.to_bytes(length, byteorder='big', length=2) + int.to_bytes(0,byteorder='big',length=4) + _savPdu


#region Protocol

class SmpCnt(univ.OctetString):
    pass

class Data(univ.OctetString):
    pass


class GmidData(univ.OctetString):
    pass


class UtcTime(univ.OctetString):
    pass


class ASDU(univ.Sequence):
    pass


ASDU.componentType = namedtype.NamedTypes(
    namedtype.NamedType('svID', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.OptionalNamedType('datSet', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1))),
    namedtype.NamedType('smpCnt', SmpCnt().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 2))),
    namedtype.NamedType('confRev', univ.Integer().subtype(subtypeSpec=constraint.ValueRangeConstraint(0, 4294967295)).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 3))),
    namedtype.OptionalNamedType('refrTm', UtcTime().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 4))),
    namedtype.OptionalNamedType('smpSynch', univ.Integer(namedValues=namedval.NamedValues(('none', 0), ('local', 1), ('global', 2))).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 5))),
    namedtype.OptionalNamedType('smpRate', univ.Integer().subtype(subtypeSpec=constraint.ValueRangeConstraint(0, 65535)).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 6))),
    namedtype.NamedType('seqData', Data().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 7))),
    namedtype.OptionalNamedType('smpMod', univ.Integer(namedValues=namedval.NamedValues(('samplesPerNormalPeriod', 0), ('samplesPerSecond', 1), ('secondsPerSample', 2))).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 8))),
    namedtype.OptionalNamedType('gmidData', GmidData().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 9)))
)


class SavPdu(univ.Sequence):
    pass


SavPdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('noASDU', univ.Integer().subtype(subtypeSpec=constraint.ValueRangeConstraint(0, 65535)).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('seqASDU', univ.SequenceOf(componentType=ASDU()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 2)))
)


class SampledValues(univ.Choice):
    pass


SampledValues.componentType = namedtype.NamedTypes(
    namedtype.NamedType('savPdu', SavPdu().subtype(implicitTag=tag.Tag(tag.tagClassApplication, tag.tagFormatConstructed, 0)))
)

#endregion