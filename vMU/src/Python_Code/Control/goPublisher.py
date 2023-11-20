#!/root/vMU/vEnv/bin/python3

from pyasn1.codec.ber import encoder
from pyasn1.type import char, namedtype, namedval, tag, univ

class GoPublisher:
    def __init__(self, AppId=0x4000,macDst='01:0c:cd:01:00:00',macSrc='01:0c:cd:01:00:00',vLan=0x8000):
        self.appId = AppId
        self.vLan = vLan
        self.macDst = macDst
        self.macSrc = macSrc
        self.goPdu = GOOSEpdu()['goosePdu']
        self.header = bytes()
        self.setHeater()

    def setHeater(self,):
        self.header = bytes()
        self.header += bytes.fromhex(self.macDst.replace(':','')) #macDst
        self.header += bytes.fromhex(self.macSrc.replace(':','')) #macSrc
        if self.vLan is not None: #vLan
            self.header += bytes.fromhex('8100')
            self.header += bytes.fromhex('{:04x}'.format(self.vLan))
        self.header += bytes.fromhex('88b8') #EtherType
        self.header += bytes.fromhex('{:04x}'.format(self.appId))

    def asduSetup(self, gocbRef, timeAllowedtoLive, datSet, t, stNum, sqNum, simulation, confRev, ndsCom, numDatSetEntries, goID = None):
        if gocbRef is not None:
            self.goPdu['gocbRef'] = gocbRef
        if timeAllowedtoLive is not None:
            self.goPdu['timeAllowedtoLive'] = timeAllowedtoLive
        if datSet is not None:
            self.goPdu['datSet'] = datSet
        if goID is not None:
            self.goPdu['goID'] = goID
        if t is not None:
            self.goPdu['t'] = t
        if stNum is not None:
            self.goPdu['stNum'] = stNum
        if sqNum is not None:
            self.goPdu['sqNum'] = sqNum
        if simulation is not None:
            self.goPdu['simulation'] = simulation
        if confRev is not None:
            self.goPdu['confRev'] = confRev
        if ndsCom is not None:
            self.goPdu['ndsCom'] = ndsCom
        if numDatSetEntries is not None:
            self.goPdu['numDatSetEntries'] = numDatSetEntries
        pass

    def changeAsduParam(self, name, value):
        self.goPdu[name] = value

    def getFrame(self, data):
        i = 0
        for dat in data:
            if type(dat) is bool:
                self.goPdu['allData'][i]['boolean'] = dat
            elif type(dat)  is int:
                self.goPdu['allData'][i]['integer'] = dat
            elif type(dat)  is float:
                self.goPdu['allData'][i]['floating-point'] = dat
            elif type(dat)  is str:
                self.goPdu['allData'][i]['mMSString'] = dat
            elif type(dat)  is bytes:
                self.goPdu['allData'][i]['octet-string'] = dat
            else:
                print("Error: data type not supported!")
            i += 1
        frame = bytes()
        frame += self.header
        frame += int.to_bytes(len(self.goPdu), byteorder='big', length=2)
        frame += int.to_bytes(0,byteorder='big',length=4)
        frame += encoder.encode(self.goPdu)
        return frame

#region Protocol

class FloatingPoint(univ.OctetString):
    pass


class UtcTime(univ.OctetString):
    pass


class TimeOfDay(univ.OctetString):
    pass


class MMSString(char.UTF8String):
    pass


class Data(univ.Choice):
    pass


Data.componentType = namedtype.NamedTypes(
    namedtype.NamedType('array', univ.SequenceOf(componentType=Data()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1))),
    namedtype.NamedType('structure', univ.SequenceOf(componentType=Data()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 2))),
    namedtype.NamedType('boolean', univ.Boolean().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 3))),
    namedtype.NamedType('bit-string', univ.BitString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 4))),
    namedtype.NamedType('integer', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 5))),
    namedtype.NamedType('unsigned', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 6))),
    namedtype.NamedType('floating-point', FloatingPoint().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 7))),
    namedtype.NamedType('real', univ.Real().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 8))),
    namedtype.NamedType('octet-string', univ.OctetString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 9))),
    namedtype.NamedType('visible-string', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 10))),
    namedtype.NamedType('binary-time', TimeOfDay().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 12))),
    namedtype.NamedType('bcd', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 13))),
    namedtype.NamedType('booleanArray', univ.BitString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 14))),
    namedtype.NamedType('objId', univ.ObjectIdentifier().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 15))),
    namedtype.NamedType('mMSString', MMSString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 16))),
    namedtype.NamedType('utc-time', UtcTime().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 17)))
)


class ErrorReason(univ.Integer):
    pass


ErrorReason.namedValues = namedval.NamedValues(
    ('other', 0),
    ('notFound', 1)
)


class IECGoosePdu(univ.Sequence):
    pass


IECGoosePdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('gocbRef', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('timeAllowedtoLive', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1))),
    namedtype.NamedType('datSet', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 2))),
    namedtype.OptionalNamedType('goID', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 3))),
    namedtype.NamedType('t', UtcTime().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 4))),
    namedtype.NamedType('stNum', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 5))),
    namedtype.NamedType('sqNum', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 6))),
    namedtype.DefaultedNamedType('simulation', univ.Boolean().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 7)).subtype(value=0)),
    namedtype.NamedType('confRev', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 8))),
    namedtype.DefaultedNamedType('ndsCom', univ.Boolean().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 9)).subtype(value=0)),
    namedtype.NamedType('numDatSetEntries', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 10))),
    namedtype.NamedType('allData', univ.SequenceOf(componentType=Data()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 11)))
)


class GetElementRequestPdu(univ.Sequence):
    pass


GetElementRequestPdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('ident', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('references', univ.SequenceOf(componentType=char.VisibleString()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1)))
)


class GetReferenceRequestPdu(univ.Sequence):
    pass


GetReferenceRequestPdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('ident', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('offset', univ.SequenceOf(componentType=univ.Integer()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1)))
)


class GSEMngtRequests(univ.Choice):
    pass


GSEMngtRequests.componentType = namedtype.NamedTypes(
    namedtype.NamedType('getGoReference', GetReferenceRequestPdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 1))),
    namedtype.NamedType('getGOOSEElementNumber', GetElementRequestPdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 2))),
    namedtype.NamedType('getGsReference', GetReferenceRequestPdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 3))),
    namedtype.NamedType('getGSSEDataOffset', GetElementRequestPdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 4)))
)


class RequestResults(univ.Choice):
    pass


RequestResults.componentType = namedtype.NamedTypes(
    namedtype.NamedType('offset', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('reference', char.IA5String().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1))),
    namedtype.NamedType('error', ErrorReason().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 2)))
)


class GlbErrors(univ.Integer):
    pass


GlbErrors.namedValues = namedval.NamedValues(
    ('other', 0),
    ('unknownControlBlock', 1),
    ('responseTooLarge', 2),
    ('controlBlockConfigurationError', 3)
)


class PositiveNegative(univ.Choice):
    pass


PositiveNegative.componentType = namedtype.NamedTypes(
    namedtype.NamedType('responsePositive', univ.Sequence(componentType=namedtype.NamedTypes(
        namedtype.OptionalNamedType('datSet', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
        namedtype.NamedType('result', univ.SequenceOf(componentType=RequestResults()).subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1)))
    ))
    .subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 2))),
    namedtype.NamedType('responseNegative', GlbErrors().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 3)))
)


class GSEMngtResponsePdu(univ.Sequence):
    pass


GSEMngtResponsePdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('ident', char.VisibleString().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.OptionalNamedType('confRev', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 1))),
    namedtype.NamedType('posNeg', PositiveNegative())
)


class GSEMngtResponses(univ.Choice):
    pass


GSEMngtResponses.componentType = namedtype.NamedTypes(
    namedtype.NamedType('gseMngtNotSupported', univ.Null().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('getGoReference', GSEMngtResponsePdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 1))),
    namedtype.NamedType('getGOOSEElementNumber', GSEMngtResponsePdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 2))),
    namedtype.NamedType('getGsReference', GSEMngtResponsePdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 3))),
    namedtype.NamedType('getGSSEDataOffset', GSEMngtResponsePdu().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 4)))
)


class RequestResponse(univ.Choice):
    pass


RequestResponse.componentType = namedtype.NamedTypes(
    namedtype.NamedType('requests', GSEMngtRequests().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 1))),
    namedtype.NamedType('responses', GSEMngtResponses().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatConstructed, 2)))
)


class GSEMngtPdu(univ.Sequence):
    pass


GSEMngtPdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('stateID', univ.Integer().subtype(implicitTag=tag.Tag(tag.tagClassContext, tag.tagFormatSimple, 0))),
    namedtype.NamedType('requestResp', RequestResponse())
)


class GOOSEpdu(univ.Choice):
    pass


GOOSEpdu.componentType = namedtype.NamedTypes(
    namedtype.NamedType('gseMngtPdu', GSEMngtPdu().subtype(implicitTag=tag.Tag(tag.tagClassApplication, tag.tagFormatConstructed, 0))),
    namedtype.NamedType('goosePdu', IECGoosePdu().subtype(implicitTag=tag.Tag(tag.tagClassApplication, tag.tagFormatConstructed, 1)))
)

#endregion


if __name__ == '__main__':
    from scapy.all import conf
    x = GoPublisher()
    x.asduSetup(gocbRef='test', timeAllowedtoLive=1, datSet='test', t=int.to_bytes(0,length=8,byteorder='big'), stNum=1, sqNum=1, simulation=0, confRev=1, ndsCom=0, numDatSetEntries=1)
    frame = x.getFrame([True, False, 1, -2, 'hello'])

    socket = conf.L2socket('Intel(R) Dual Band Wireless-AC 7265')
    for i in range(0,100):
        socket.send(frame)