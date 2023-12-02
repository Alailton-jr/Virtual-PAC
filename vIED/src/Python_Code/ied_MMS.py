#!/root/Virtual-PAC/vIED/vEnv/bin/python3

# unfinished

import os, xmltodict
from lxml import etree
import yaml

iedName = 'vIED_0'

class MMS_Server:
    def __init__(self) -> None:
        self.iedName = None
        self.data = None
        self.sharedMemory = []

    def loadSCL(self, filePath):
        with open('vIed.scd') as fd:
            x = xmltodict.parse(fd.read())

        self.iedName = x['SCL']['IED']['@name']

        LdeviceList = x['SCL']['IED']['AccessPoint']['Server']['LDevice']

        LDevice = dict()
        for ld in LdeviceList:
            LDevice[ld['@inst']] = list()
            if type(ld['LN']) is list:
                for ln in ld['LN']:
                    if '@lnType' not in ln:
                        ln['@lnType'] = 'None'
                    conf = {'lnType': ln['@lnType'], 'lnClass': ln['@lnClass'], 'prefix': ln['@prefix']}
                    LDevice[ld['@inst']].append(conf)
            else:
                ln = ld['LN']
                conf = {'lnType': ln['@lnType'], 'lnClass': ln['@lnClass'], 'prefix': ln['@prefix']}
                LDevice[ld['@inst']].append(conf)
        
        def findType(typeId, data):
            flag = False
            for doType in x['SCL']['DataTypeTemplates']['DOType']:
                if typeId == doType['@id']:
                    flag = True
                    if 'DA' in doType and type(doType['DA']) is list:
                        for da in doType['DA']:
                            data[da['@name']] = {}
                            if '@type' in da:
                                findType(da['@type'], data[da['@name']])
                            else:
                                data[da['@name']]['bType'] = da['@bType']
                                data[da['@name']]['Value'] = None
                    elif 'SDO' in doType and type(doType['SDO']) is list:
                        for da in doType['SDO']:
                            data[da['@name']] = {}
                            if '@type' in da:
                                findType(da['@type'], data[da['@name']])
                            else:
                                data[da['@name']]['bType'] = da['@bType']
                                data[da['@name']]['Value'] = None
                    else:
                        if 'DA' in doType:
                            bda = doType['DA']
                        else:
                            bda = doType['SDO']
                        if '@type' in bda:
                            data[bda['@name']] = {}
                            findType(bda['@type'], data[bda['@name']])
                        else:
                            data[data[bda['@name']]]['bType'] = bda['@bType']
                            data[data[bda['@name']]]['Value'] = None
            if flag:
                return
            
            for daType in x['SCL']['DataTypeTemplates']['DAType']:
                if typeId == daType['@id']:
                    flag = True
                    if type(daType['BDA']) is list:
                        for bda in daType['BDA']:
                            data[bda['@name']] = {}
                            if '@type' in bda:
                                findType(bda['@type'], data[bda['@name']])
                            else:
                                data[bda['@name']]['bType'] = bda['@bType']
                                data[bda['@name']]['Value'] = None
                    else:
                        bda = daType['BDA']
                        data[bda['@name']] = {}
                        if '@type' in bda:
                            findType(bda['@type'], data[bda['@name']])
                        else:
                            data[bda['@name']]['bType'] = bda['@bType']
                            data[bda['@name']]['Value'] = None
            if flag:
                return

            for enumType in x['SCL']['DataTypeTemplates']['EnumType']:
                if typeId == enumType['@id']:
                    flag = True
                    data['Value'] = None
                    data['bType'] = 'Enum'
                    data['Enum'] = {}
                    for enum in enumType['EnumVal']:
                        if '#text' in enum:
                            data['Enum'][enum['#text']] = int(enum['@ord'])
                        else:
                            data['Enum'][' '] = int(enum['@ord'])
                        

        self.data = {}
        lnTypes =x['SCL']['DataTypeTemplates']['LNodeType']
        for ldName, ld in LDevice.items():
            for ln in ld:
                self.data[ln['lnClass']+'/'+ln['prefix']+ln['lnType']] = {}
                temp = self.data[ln['lnClass']+'/'+ln['prefix']+ln['lnType']]
                for lnTypes in x['SCL']['DataTypeTemplates']['LNodeType']:
                    if lnTypes['@id'] == ln['lnType']:
                        for do in lnTypes['DO']:
                            temp[do['@name']] = {}
                            findType(do['@type'], temp[do['@name']])

    def configDefault(self):
        self.data['']

def main():
    x = MMS_Server()
    x.loadSCL('vIed.scd')
    data = x.data

    print('done')


main()


# x = None
# with open('vIed.scd') as fd:
#     x = xmltodict.parse(fd.read())



# LdeviceList = x['SCL']['IED']['AccessPoint']['Server']['LDevice']

# LDevice = dict()
# for ld in LdeviceList:
#     LDevice[ld['@inst']] = list()
#     if type(ld['LN']) is list:
#         for ln in ld['LN']:
#             if '@lnType' not in ln:
#                 ln['@lnType'] = 'None'
#             conf = {'lnType': ln['@lnType'], 'lnClass': ln['@lnClass'], 'prefix': ln['@prefix']}
#             LDevice[ld['@inst']].append(conf)
#     else:
#         ln = ld['LN']
#         conf = {'lnType': ln['@lnType'], 'lnClass': ln['@lnClass'], 'prefix': ln['@prefix']}
#         LDevice[ld['@inst']].append(conf)


# def findType(typeId, data):
#     flag = False
#     for doType in x['SCL']['DataTypeTemplates']['DOType']:
#         if typeId == doType['@id']:
#             flag = True
#             if 'DA' in doType and type(doType['DA']) is list:
#                 for da in doType['DA']:
#                     data[da['@name']] = {}
#                     if '@type' in da:
#                         findType(da['@type'], data[da['@name']])
#                     else:
#                         data[da['@name']]['bType'] = da['@bType']
#             elif 'SDO' in doType and type(doType['SDO']) is list:
#                 for da in doType['SDO']:
#                     data[da['@name']] = {}
#                     if '@type' in da:
#                         findType(da['@type'], data[da['@name']])
#                     else:
#                         data[da['@name']]['bType'] = da['@bType']
#             else:
#                 if 'DA' in doType:
#                     bda = doType['DA']
#                 else:
#                     bda = doType['SDO']
#                 if '@type' in bda:
#                     data[bda['@name']] = {}
#                     findType(bda['@type'], data[bda['@name']])
#                 else:
#                     data[data[bda['@name']]]['bType'] = bda['@bType']
#                     data[bda['@name']]['Value'] = None
#     if flag:
#         return
    
#     for daType in x['SCL']['DataTypeTemplates']['DAType']:
#         if typeId == daType['@id']:
#             flag = True
#             if type(daType['BDA']) is list:
#                 for bda in daType['BDA']:
#                     data[bda['@name']] = {}
#                     if '@type' in bda:
#                         findType(bda['@type'], data[bda['@name']])
#                     else:
#                         data[bda['@name']]['bType'] = bda['@bType']
#                         data[bda['@name']]['Value'] = None
#             else:
#                 bda = daType['BDA']
#                 data[bda['@name']] = {}
#                 if '@type' in bda:
#                     findType(bda['@type'], data[bda['@name']])
#                 else:
#                     data[bda['@name']]['bType'] = bda['@bType']
#                     data[bda['@name']]['Value'] = None
#     if flag:
#         return

#     for enumType in x['SCL']['DataTypeTemplates']['EnumType']:
#         if typeId == enumType['@id']:
#             flag = True
#             data['Enum'] = {}
#             for enum in enumType['EnumVal']:
#                 if '#text' in enum:
#                     data['Enum'][enum['#text']] = int(enum['@ord'])
#                 else:
#                     data['Enum'][' '] = int(enum['@ord'])

# data = {}
# lnTypes =x['SCL']['DataTypeTemplates']['LNodeType']
# for ldName, ld in LDevice.items():
#     for ln in ld:
#         data[ln['lnClass']+'/'+ln['prefix']+ln['lnType']] = {}
#         temp = data[ln['lnClass']+'/'+ln['prefix']+ln['lnType']]
#         for lnTypes in x['SCL']['DataTypeTemplates']['LNodeType']:
#             if lnTypes['@id'] == ln['lnType']:
#                 for do in lnTypes['DO']:
#                     temp[do['@name']] = {}
#                     findType(do['@type'], temp[do['@name']])

# print('done')







    
# class SCL_handler:
#     def __init__(self) -> None:
#         self.filePath = None
#         self.scl = None

#     def loadSCL(self, filePath):
#         self.filePath = filePath
#         tree = etree.parse(filePath, base_url='http://www.iec.ch/61850/2003/SCL')
#         root = tree.getroot()
#         self.scl = self.xmlParse(root)

#     def xmlParse(self, parent):
#         var = {'attributes': None}
#         var['attributes'] = dict(parent.attrib)

#         for chield in parent:
#             _chield = self.xmlParse(chield)
#             if str(chield.tag).endswith('Private') or  '{http://www.iec.ch/61850/2003/SCL}' not in str(chield.tag):
#                 continue
#             tag = str(chield.tag).replace('{http://www.iec.ch/61850/2003/SCL}','')
#             name = [s for s in chield.attrib if 'name' in s or 'Name' in s]
#             if not name:
#                 name = [s for s in chield.attrib if 'inst' in s]
#             if name:
#                 nameParam = str(chield.attrib[name[0]])
#             if tag in ['LN0', 'FCDA']:
#                 name = None
#             elif tag in ['LNodeType', 'DOType', 'DAType', 'EnumType']:
#                 nameParam = str(chield.attrib['id'])
#                 name = nameParam
#             elif tag in ['EnumVal']:
#                 nameParam = str(chield.attrib['ord'])
#                 name = nameParam
#                 _chield['text'] = chield.text

#             elif tag == 'LN':
#                 name = [str(chield.attrib.get('lnClass')) + str(chield.attrib.get('inst')) + str(chield.attrib.get('prefix'))]
#                 nameParam = str(name[0])
            
#             if name:
#                 name = name[0]
#                 if tag in var:
#                     if type(var[tag]) is list:
#                         var[tag].append({str(nameParam): _chield})
#                     else:
#                         var[tag].update({str(nameParam): _chield})
#                 else:
#                     var[tag] = {nameParam: _chield}
#             else:
#                 if tag in var:
#                     if type(var[tag]) is list:
#                         var[tag].append(_chield)
#                     else:
#                         var[tag] = [var[tag], _chield]
#                 else:
#                     var[tag] = _chield
            
#         return var

#     def _findType(self, _id):
#         if _id in self.scl['DataTypeTemplates']['DAType']:
#             daType = self.scl['DataTypeTemplates']['DAType'][_id]
#             bdaList = []
#             for _bda in daType['BDA'].values():
#                 bda = _bda['attributes']
#                 if 'type' in bda:
#                     bdaList.append({'bType': bda['bType'], 'type': self._findType(bda['type']), 'name': bda['name']})
#                 else:
#                     bdaList.append({'bType': bda['bType'], 'name': bda['name']})
#             return bdaList
#         elif _id in self.scl['DataTypeTemplates']['DOType']:
#             doType = self.scl['DataTypeTemplates']['DOType'][_id]
#             daList = []
#             if 'DA' in doType:
#                 for _da in doType['DA'].values():
#                     da = _da['attributes']
#                     if 'type' in da:
#                         daList.append({'bType': da['bType'], 'type': self._findType(da['type']), 'name': da['name']})
#                     else:
#                         daList.append({'bType': da['bType'], 'name': da['name']})
#                 return daList
#             elif 'SDO' in doType:
#                 for _sdo in doType['SDO'].values():
#                     sdo = _sdo['attributes']
#                     if 'type' in sdo:
#                         daList.append({'type': self._findType(sdo['type']), 'name': sdo['name']})
#                 return daList
#         else:
#             enumType = self.scl['DataTypeTemplates']['EnumType'][_id]
#             enumList = []
#             for _ord, _text in enumType['EnumVal'].items():
#                 # enumList.append({'ord': _ord, 'text': _text})
#                 enumList.append([_ord, _text['text']])
#             return enumList

#     def findGse(self,):
#         self.gseList = []
#         for key, subnet in self.scl['Communication']['SubNetwork'].items():
#             if 'ConnectedAP' in subnet:
#                 for ieds in subnet['ConnectedAP'].values():
#                     if 'GSE' in ieds:
#                         for gse in ieds['GSE'].values():
#                             gse.update(ieds['attributes'])
#                             gse.update(gse['attributes'])
#                         self.gseList.append(gse)
#         self.goList = []


#         for gse in self.gseList:
#             Ld = self.scl['IED'][gse['iedName']]['AccessPoint'][gse['apName']]['Server']['LDevice']
#             if 'GSEControl' not in Ld[gse['ldInst']]['LN0']:
#                 continue
#             gseCtl = Ld[gse['ldInst']]['LN0']['GSEControl'][gse['cbName']]['attributes']
#             dataSet = Ld[gse['ldInst']]['LN0']['DataSet'][gseCtl['datSet']]
#             _go = dict(gseCtl)
#             _go['data'] = []
#             for fcda in dataSet['FCDA']:
#                 fcda = fcda['attributes']
#                 lntype = Ld[fcda['ldInst']]['LN'][fcda.get('lnClass') + fcda.get('lnInst') + fcda.get('prefix')]['attributes']['lnType']
#                 do = self.scl['DataTypeTemplates']['LNodeType'][lntype]['DO'][fcda['doName']]['attributes']
#                 fcda['cdc'] = self.scl['DataTypeTemplates']['DOType'][do['type']]['attributes']['cdc']
#                 if 'daName' in fcda:
#                     da =  self.scl['DataTypeTemplates']['DOType'][do['type']]['DA'][fcda['daName']]['attributes']
#                     fcda['bType'] = da['bType']
#                     if 'type' in da['bType']:
#                         fcda['type'] = self._findType(da['type'])
#                 else:
#                     fcda['bType'] = 'Struct'
#                     fcda['type'] = []
#                     for _da in self.scl['DataTypeTemplates']['DOType'][do['type']]['DA'].values():
#                         da = _da['attributes']
#                         if fcda['fc'] != da['fc']:
#                             continue
#                         if 'type' in da:
#                             fcda['type'] = self._findType(da['type'])
#                         else:
#                             fcda['type'].append({'bType': da['bType'], 'name': da['name']})
#                 _go['data'].append(fcda)
#             self.goList.append(_go)
#         return self.goList

#     def findSmv(self,):
#         self.smvList = []
#         for key, subnet in self.scl['Communication']['SubNetwork'].items():
#             if 'ConnectedAP' in subnet:
#                 for ieds in subnet['ConnectedAP'].values():
#                     if 'SMV' in ieds:
#                         for smv in ieds['SMV'].values():
#                             smv.update(ieds['attributes'])
#                             smv.update(smv['attributes'])
#                         self.smvList.append(smv)

#         self.svList = []

#         for smv in self.smvList:
#             Ld = self.scl['IED'][smv['iedName']]['AccessPoint'][smv['apName']]['Server']['LDevice']
#             if 'SampledValueControl' not in Ld[smv['ldInst']]['LN0']:
#                 continue
#             smvCtl = Ld[smv['ldInst']]['LN0']['SampledValueControl'][smv['cbName']]['attributes']
#             dataSet = Ld[smv['ldInst']]['LN0']['DataSet'][smvCtl['datSet']]
#             _sv = dict(smvCtl)
#             _sv['data'] = []
#             for fcda in dataSet['FCDA']:
#                 fcda = fcda['attributes']
#                 lntype = Ld[fcda['ldInst']]['LN'][fcda.get('lnClass') + fcda.get('lnInst') + fcda.get('prefix')]['attributes']['lnType']
#                 do = self.scl['DataTypeTemplates']['LNodeType'][lntype]['DO'][fcda['doName']]['attributes']
#                 fcda['cdc'] = self.scl['DataTypeTemplates']['DOType'][do['type']]['attributes']['cdc']
#                 if 'daName' in fcda:
#                     if '.' in fcda['daName']:
#                         fcda['daName'] = fcda['daName'].split('.')[0]
#                     da =  self.scl['DataTypeTemplates']['DOType'][do['type']]['DA'][fcda['daName']]['attributes']
#                     fcda['bType'] = da['bType']
#                     if 'type' in da:
#                         fcda['type'] = self._findType(da['type'])
#                 else:
#                     fcda['bType'] = 'Struct'
#                     fcda['type'] = []
#                     for _da in self.scl['DataTypeTemplates']['DOType'][do['type']]['DA'].values():
#                         da = _da['attributes']
#                         if fcda['fc'] != da['fc']:
#                             continue
#                         if 'type' in da:
#                             fcda['type'] = self._findType(da['type'])
#                         else:
#                             fcda['type'].append({'bType': da['bType'], 'name': da['name']})
#                 _sv['data'].append(fcda)

#             self.svList.append(_sv)

#         return self.svList
    

# tree = etree.parse('vIed.scd')
# root = tree.getroot()
# class XmlClass:
#     def __init__(self, node) -> None:
#         self.root = node
#         nsmap = {'ns': 'http://www.iec.ch/61850/2003/SCL'}
#         self.xpath = etree.XPath('local-name()', namespaces=nsmap)
#         pass

#     def getNode(self, key):
#         for child in self.root:
#             if self.xpath(child) == key:
#                 return child
#         return None

#     def getAttri(self, key, value):
#         for child in self.root:
#             if key in child.attrib and child.attrib[key] == value:
#                 return XmlClass(child)
#         return None

#     def __getitem__(self, key):
#         for child in self.root:
#             if self.xpath(child) == key:
#                 return XmlClass(child)
#         return None
