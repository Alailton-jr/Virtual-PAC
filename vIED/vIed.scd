<?xml version="1.0" encoding="utf-8"?>
<SCL version="2007" revision="B" release="4" xmlns="http://www.iec.ch/61850/2003/SCL">
  <Header id="Virtual IED" version="1" revision="1.0" toolID="UFU_vSCL" />
  <Communication>
    <SubNetwork name="vNetwork">
      <ConnectedAP iedName="vIED_0" apName="vNet">
        <Address>
          <P type="IP">10.3.193.100</P>
          <P type="IP-SUBNET">255.255.255.0</P>
          <P type="IP-GATEWAY">10.3.192.1</P>
          <P type="OSI-TSEL">0001</P>
          <P type="OSI-PSEL">00000001</P>
          <P type="OSI-SSEL">0001</P>
        </Address>
      </ConnectedAP>
    </SubNetwork>
  </Communication>
  <IED name="vIED_0" type="vIED" manufacturer="LRI" configVersion="1.0">
    <AccessPoint name="vNet">
      <Server>
        <Authentication />
        <LDevice desc="Data Sets" inst="CFG">
          <LN0 lnType="LN0_1" lnClass="LLN0" inst="">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
          </LN0>
          <LN lnType="LPHD_1" prefix="vID" lnClass="LPHD" inst="1">
            <DOI name="PhyNam">
              <DAI name="vendor" />
              <DAI name="serNum" />
              <DAI name="model" />
            </DOI>
            <DOI name="PhyHealth">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Proxy">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
          </LN>
        </LDevice>
        <LDevice desc="Protection" inst="PROT">
          <LN0 lnType="LN0_1" lnClass="LLN0" inst="">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
          </LN0>
          <LN lnType="LPHD_1" prefix="vID" lnClass="LPHD" inst="1">
            <DOI name="PhyNam">
              <DAI name="vendor" />
              <DAI name="serNum" />
              <DAI name="model" />
            </DOI>
            <DOI name="PhyHealth">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Proxy">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="P1" lnClass="PIOC" inst="1">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="P2" lnClass="PIOC" inst="2">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="P3" lnClass="PIOC" inst="3">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="N1" lnClass="PIOC" inst="4">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="N2" lnClass="PIOC" inst="5">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PIOC_1" prefix="N3" lnClass="PIOC" inst="6">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="P1" lnClass="PTOC" inst="1">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="P2" lnClass="PTOC" inst="2">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="P3" lnClass="PTOC" inst="3">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="N1" lnClass="PTOC" inst="4">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="N2" lnClass="PTOC" inst="5">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTOC_1" prefix="N3" lnClass="PTOC" inst="6">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="Str">
              <DAI name="general" />
              <DAI name="dirGeneral" />
              <DAI name="q" />
            </DOI>
            <DOI name="Op">
              <DAI name="general" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="PTRC_1" prefix="TRIP" lnClass="PTRC" inst="1" />
        </LDevice>
        <LDevice desc="Metering" inst="MET">
          <LN0 lnType="LN0_1" lnClass="LLN0" inst="">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
          </LN0>
          <LN lnType="LPHD_1" prefix="vID" lnClass="LPHD" inst="1">
            <DOI name="PhyNam">
              <DAI name="vendor" />
              <DAI name="serNum" />
              <DAI name="model" />
            </DOI>
            <DOI name="PhyHealth">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Proxy">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
          </LN>
          <LN lnType="MMXU_1" prefix="MET" lnClass="MMXU" inst="1">
            <DOI name="Mod">
              <DAI name="stVal" />
              <DAI name="q" />
              <DAI name="cltModel" />
            </DOI>
            <DOI name="Health">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="Beh">
              <DAI name="stVal" />
              <DAI name="q" />
            </DOI>
            <DOI name="NamPlt">
              <DAI name="vendor" />
              <DAI name="swRev" />
              <DAI name="d" />
              <DAI name="configRev" />
              <DAI name="ldNs" />
            </DOI>
            <DOI name="TotW">
              <SDI name="instMag">
                <DAI name="f" />
              </SDI>
              <DAI name="q" />
              <SDI name="units">
                <DAI name="SIUnit">
                  <Val>Watts</Val>
                </DAI>
                <DAI name="multiplier">
                  <Val>k</Val>
                </DAI>
              </SDI>
              <DAI name="db">
                <Val>100</Val>
              </DAI>
            </DOI>
            <DOI name="TotVAr">
              <SDI name="instMag">
                <DAI name="f" />
              </SDI>
              <DAI name="q" />
              <SDI name="units">
                <DAI name="SIUnit">
                  <Val>VAr</Val>
                </DAI>
                <DAI name="multiplier">
                  <Val>k</Val>
                </DAI>
              </SDI>
              <DAI name="db">
                <Val>100</Val>
              </DAI>
            </DOI>
            <DOI name="TotVA">
              <SDI name="instMag">
                <DAI name="f" />
              </SDI>
              <DAI name="q" />
              <SDI name="units">
                <DAI name="SIUnit">
                  <Val>VA</Val>
                </DAI>
                <DAI name="multiplier">
                  <Val>k</Val>
                </DAI>
              </SDI>
              <DAI name="db">
                <Val>100</Val>
              </DAI>
            </DOI>
            <DOI name="Hz">
              <SDI name="instMag">
                <DAI name="f" />
              </SDI>
              <DAI name="q" />
              <SDI name="units">
                <DAI name="SIUnit">
                  <Val>Hz</Val>
                </DAI>
                <DAI name="multiplier">
                  <Val></Val>
                </DAI>
              </SDI>
              <DAI name="db">
                <Val>500</Val>
              </DAI>
            </DOI>
            <DOI name="PPV">
              <SDI name="phsAB">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsBC">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsCA">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
            </DOI>
            <DOI name="PhV">
              <SDI name="phsA">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsB">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsC">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="res">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
            </DOI>
            <DOI name="A1">
              <SDI name="phsA">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsB">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsC">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="res">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
            </DOI>
            <DOI name="A2">
              <SDI name="phsA">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsB">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="phsC">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
              <SDI name="res">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
            </DOI>
            <DOI name="A">
              <SDI name="neut">
                <SDI name="instCVal">
                  <SDI name="mag">
                    <DAI name="f" />
                  </SDI>
                  <SDI name="ang">
                    <DAI name="f" />
                  </SDI>
                </SDI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>V</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val></Val>
                  </DAI>
                </SDI>
                <DAI name="q" />
                <DAI name="db">
                  <Val>50</Val>
                </DAI>
                <SDI name="units">
                  <DAI name="SIUnit">
                    <Val>Hz</Val>
                  </DAI>
                  <DAI name="multiplier">
                    <Val>50</Val>
                  </DAI>
                </SDI>
              </SDI>
            </DOI>
          </LN>
        </LDevice>
      </Server>
    </AccessPoint>
  </IED>
  <DataTypeTemplates>
    <LNodeType id="LN0_1" lnClass="LLN0">
      <DO name="Mod" type="modENC" />
      <DO name="Beh" type="behENS" />
      <DO name="Health" type="healthENS" />
      <DO name="NamPlt" type="LPL_LN0" />
    </LNodeType>
    <LNodeType id="LPHD_1" lnClass="LPHD">
      <DO name="PhyNam" type="DPL_1" />
      <DO name="PhyHealth" type="healthENS" />
      <DO name="Proxy" type="SPS_1" />
    </LNodeType>
    <LNodeType id="PIOC_1" desc="ANSI 50" lnClass="PIOC">
      <DO name="Mod" type="modENC" />
      <DO name="Beh" type="behENS" />
      <DO name="Health" type="healthENS" />
      <DO name="NamPlt" type="LPL_1" />
      <DO name="Str" type="ACD_1" />
      <DO name="Op" type="ACT_1" />
    </LNodeType>
    <LNodeType id="PTOC_1" desc="ANSI 51" lnClass="PTOC">
      <DO name="Mod" type="modENC" />
      <DO name="Beh" type="behENS" />
      <DO name="Health" type="healthENS" />
      <DO name="NamPlt" type="LPL_1" />
      <DO name="Str" type="ACD_1" />
      <DO name="Op" type="ACT_1" />
    </LNodeType>
    <LNodeType id="MMXU_1" desc="ANSI 51" lnClass="MMXU">
      <DO name="Mod" type="modENC" />
      <DO name="Beh" type="behENS" />
      <DO name="Health" type="healthENS" />
      <DO name="NamPlt" type="LPL_1" />
      <DO name="TotW" type="analogMV_1" />
      <DO name="TotVAr" type="analogMV_1" />
      <DO name="TotVA" type="analogMV_1" />
      <DO name="Hz" type="analogMV_1" />
      <DO name="PPV" type="DEL_1" />
      <DO name="PhV" type="WYE_1" />
      <DO name="A1" type="WYE_1" />
      <DO name="A2" type="WYE_1" />
      <DO name="A" type="WYE_2" />
    </LNodeType>
    <LNodeType id="PTRC_1" desc="Trip" lnClass="PTRC">
      <DO name="Mod" type="modENC" />
      <DO name="Beh" type="behENS" />
      <DO name="Health" type="healthENS" />
      <DO name="NamPlt" type="LPL_1" />
      <DO name="Tr" type="ACT_1" />
      <DO name="Str" type="ACD_1" />
      <DO name="Op" type="ACT_1" />
    </LNodeType>
    <DOType id="modENC" cdc="ENC">
      <DA name="stVal" bType="Enum" type="Mod" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
      <DA name="ctlModel" bType="Enum" type="ctlModel" fc="CF" />
    </DOType>
    <DOType id="behENS" cdc="ENS">
      <DA name="stVal" bType="Enum" type="Beh" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
    </DOType>
    <DOType id="healthENS" cdc="ENS">
      <DA name="stVal" bType="Enum" type="Health" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
    </DOType>
    <DOType id="LPL_LN0" cdc="LPL">
      <DA name="vendor" bType="VisString255" fc="DC" />
      <DA name="swRev" bType="VisString255" fc="DC" />
      <DA name="d" bType="VisString255" fc="DC" />
      <DA name="configRev" bType="VisString255" fc="DC" />
      <DA name="ldNs" bType="VisString255" fc="EX" />
    </DOType>
    <DOType id="DPL_1" cdc="DPL">
      <DA name="vendor" bType="VisString255" fc="DC" />
      <DA name="serNum" bType="VisString255" fc="DC" />
      <DA name="model" bType="VisString255" fc="DC" />
    </DOType>
    <DOType id="SPS_1" cdc="SPS">
      <DA name="stVal" bType="BOOLEAN" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
    </DOType>
    <DOType id="LPL_1" cdc="LPL">
      <DA name="vendor" bType="VisString255" fc="DC" />
      <DA name="swRev" bType="VisString255" fc="DC" />
      <DA name="d" bType="VisString255" fc="DC" />
      <DA name="configRev" bType="VisString255" fc="DC" />
    </DOType>
    <DOType id="ACD_1" cdc="ACD">
      <DA name="general" bType="BOOLEAN" dchg="true" fc="ST" />
      <DA name="dirGeneral" bType="Enum" type="dir" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
      <DA name="d" bType="VisString255" fc="DC" />
    </DOType>
    <DOType id="ACT_1" cdc="ACT">
      <DA name="general" bType="BOOLEAN" dchg="true" fc="ST" />
      <DA name="q" bType="Quality" qchg="true" fc="ST" />
      <DA name="t" bType="Timestamp" fc="ST" />
      <DA name="d" bType="VisString255" fc="DC" />
    </DOType>
    <DOType id="analogMV_1" cdc="MV">
      <DA name="instMag" bType="Struct" type="AnalogValue_1" fc="MX" />
      <DA name="mag" bType="Struct" type="AnalogValue_1" dchg="true" fc="MX" />
      <DA name="q" bType="Quality" qchg="true" fc="MX" />
      <DA name="t" bType="Timestamp" fc="MX" />
      <DA name="units" bType="Struct" type="Units_1" fc="CF" />
      <DA name="db" bType="INT32U" fc="CF" />
      <DA name="d" bType="VisString255" fc="DC" />
    </DOType>
    <DOType id="DEL_1" cdc="DEL">
      <SDO name="phsAB" type="CMV_1" />
      <SDO name="phsBC" type="CMV_1" />
      <SDO name="phsCA" type="CMV_1" />
    </DOType>
    <DOType id="CMV_1" cdc="CMV">
      <DA name="instCVal" bType="Struct" type="Vector_1" fc="MX" />
      <DA name="cVal" bType="Struct" type="Vector_1" dchg="true" fc="MX" />
      <DA name="q" bType="Quality" qchg="true" fc="MX" />
      <DA name="t" bType="Timestamp" fc="MX" />
      <DA name="units" bType="Struct" type="Units_1" fc="CF" />
      <DA name="db" bType="INT32U" fc="CF" />
    </DOType>
    <DOType id="WYE_1" cdc="WYE">
      <SDO name="phsA" type="CMV_1" />
      <SDO name="phsB" type="CMV_1" />
      <SDO name="phsC" type="CMV_1" />
      <SDO name="res" type="CMV_1" />
    </DOType>
    <DOType id="WYE_2" cdc="WYE">
      <SDO name="neut" type="CMV_1" />
    </DOType>
    <DAType id="AnalogValue_1">
      <BDA name="f" bType="FLOAT32" />
    </DAType>
    <DAType id="Units_1">
      <BDA name="SIUnit" bType="Enum" type="SIUnit_1" />
      <BDA name="multiplier" bType="Enum" type="multiplier" />
    </DAType>
    <DAType id="Vector_1">
      <BDA name="mag" bType="Struct" type="AnalogValue_1" />
      <BDA name="ang" bType="Struct" type="AnalogValue_1" />
    </DAType>
    <EnumType id="Mod">
      <EnumVal ord="1">on</EnumVal>
      <EnumVal ord="2">blocked</EnumVal>
      <EnumVal ord="3">test</EnumVal>
      <EnumVal ord="4">test/blocked</EnumVal>
      <EnumVal ord="5">off</EnumVal>
    </EnumType>
    <EnumType id="ctlModel">
      <EnumVal ord="1">status-only</EnumVal>
      <EnumVal ord="2">direct-with-normal-security</EnumVal>
      <EnumVal ord="3">sbo-with-normal-security</EnumVal>
      <EnumVal ord="4">direct-with-enhanced-security</EnumVal>
      <EnumVal ord="5">sbo-with-enhanced-security</EnumVal>
    </EnumType>
    <EnumType id="Beh">
      <EnumVal ord="1">on</EnumVal>
      <EnumVal ord="2">blocked</EnumVal>
      <EnumVal ord="3">test</EnumVal>
      <EnumVal ord="4">test/blocked</EnumVal>
      <EnumVal ord="5">off</EnumVal>
    </EnumType>
    <EnumType id="Health">
      <EnumVal ord="1">Ok</EnumVal>
      <EnumVal ord="2">Warning</EnumVal>
      <EnumVal ord="3">Alarm</EnumVal>
    </EnumType>
    <EnumType id="dir">
      <EnumVal ord="0">unknown</EnumVal>
      <EnumVal ord="1">forward</EnumVal>
      <EnumVal ord="2">backward</EnumVal>
      <EnumVal ord="3">both</EnumVal>
    </EnumType>
    <EnumType id="SIUnit_1">
      <EnumVal ord="1"></EnumVal>
      <EnumVal ord="2">m</EnumVal>
      <EnumVal ord="3">kg</EnumVal>
      <EnumVal ord="4">s</EnumVal>
      <EnumVal ord="5">A</EnumVal>
      <EnumVal ord="6">K</EnumVal>
      <EnumVal ord="7">mol</EnumVal>
      <EnumVal ord="8">cd</EnumVal>
      <EnumVal ord="9">deg</EnumVal>
      <EnumVal ord="10">rad</EnumVal>
      <EnumVal ord="11">sr</EnumVal>
      <EnumVal ord="21">Gy</EnumVal>
      <EnumVal ord="22">q</EnumVal>
      <EnumVal ord="23">°C</EnumVal>
      <EnumVal ord="24">Sv</EnumVal>
      <EnumVal ord="25">F</EnumVal>
      <EnumVal ord="26">C</EnumVal>
      <EnumVal ord="27">S</EnumVal>
      <EnumVal ord="28">H</EnumVal>
      <EnumVal ord="29">V</EnumVal>
      <EnumVal ord="30">ohm</EnumVal>
      <EnumVal ord="31">J</EnumVal>
      <EnumVal ord="32">N</EnumVal>
      <EnumVal ord="33">Hz</EnumVal>
      <EnumVal ord="34">lx</EnumVal>
      <EnumVal ord="35">Lm</EnumVal>
      <EnumVal ord="36">Wb</EnumVal>
      <EnumVal ord="37">T</EnumVal>
      <EnumVal ord="38">W</EnumVal>
      <EnumVal ord="39">Pa</EnumVal>
      <EnumVal ord="41">m²</EnumVal>
      <EnumVal ord="42">m³</EnumVal>
      <EnumVal ord="43">m/s</EnumVal>
      <EnumVal ord="44">m/s²</EnumVal>
      <EnumVal ord="45">m³/s</EnumVal>
      <EnumVal ord="46">m/m³</EnumVal>
      <EnumVal ord="47">M</EnumVal>
      <EnumVal ord="48">kg/m³</EnumVal>
      <EnumVal ord="49">m²/s</EnumVal>
      <EnumVal ord="50">W/m K</EnumVal>
      <EnumVal ord="51">J/K</EnumVal>
      <EnumVal ord="52">ppm</EnumVal>
      <EnumVal ord="53">1/s</EnumVal>
      <EnumVal ord="54">rad/s</EnumVal>
      <EnumVal ord="61">VA</EnumVal>
      <EnumVal ord="62">Watts</EnumVal>
      <EnumVal ord="63">VAr</EnumVal>
      <EnumVal ord="64">phi</EnumVal>
      <EnumVal ord="65">cos(phi)</EnumVal>
      <EnumVal ord="66">Vs</EnumVal>
      <EnumVal ord="67">V²</EnumVal>
      <EnumVal ord="68">As</EnumVal>
      <EnumVal ord="69">A²</EnumVal>
      <EnumVal ord="70">A²t</EnumVal>
      <EnumVal ord="71">VAh</EnumVal>
      <EnumVal ord="72">Wh</EnumVal>
      <EnumVal ord="73">VArh</EnumVal>
      <EnumVal ord="74">V/Hz</EnumVal>
    </EnumType>
    <EnumType id="multiplier">
      <EnumVal ord="-24">y</EnumVal>
      <EnumVal ord="-21">z</EnumVal>
      <EnumVal ord="-18">a</EnumVal>
      <EnumVal ord="-15">f</EnumVal>
      <EnumVal ord="-12">p</EnumVal>
      <EnumVal ord="-9">n</EnumVal>
      <EnumVal ord="-6">µ</EnumVal>
      <EnumVal ord="-3">m</EnumVal>
      <EnumVal ord="-2">c</EnumVal>
      <EnumVal ord="-1">d</EnumVal>
      <EnumVal ord="0"></EnumVal>
      <EnumVal ord="1">da</EnumVal>
      <EnumVal ord="2">h</EnumVal>
      <EnumVal ord="3">k</EnumVal>
      <EnumVal ord="6">M</EnumVal>
      <EnumVal ord="9">G</EnumVal>
      <EnumVal ord="12">T</EnumVal>
      <EnumVal ord="15">P</EnumVal>
      <EnumVal ord="18">E</EnumVal>
      <EnumVal ord="21">Z</EnumVal>
      <EnumVal ord="24">Y</EnumVal>
    </EnumType>
  </DataTypeTemplates>
</SCL>