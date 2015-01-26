<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:key name="ISSUETYPES" match="/Report/Issues/Project/Issue" use="@TypeId"/>
    <xsl:output method="html" indent="yes"/>

    
    <xsl:template match="/" name="TopLevelReport">
        <html>
            <head>
                <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css" />
                <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-theme.min.css" />
                <style>
                    #top-link-block.affix-top {
                        position: absolute; /* allows it to "slide" up into view */
                        bottom: -82px; /* negative of the offset - height of link element */
                        right: 20px; /* padding from the left side of the window */
                    }
                    #top-link-block.affix {
                        position: fixed; /* keeps it on the bottom once in view */
                        bottom: 18px; /* height of link element */
                        right: 20px; /* padding from the left side of the window */
                    }
                </style>
            </head>
            <body>
                <a name="top"></a>
                <div class="container">
                    <div class="page-header">
                        <h1>
                            Resharper Code Inspection Report
                        </h1>
                    </div>

                    <div class="row">
                        <div class="col-md-8">
                            <a href="#errors" class="btn btn-default btn-lg btn-danger" style="margin:15px;">
                                Errors: <xsl:value-of select="count(//Report/IssueTypes/IssueType[@Severity='ERROR'])"/>
                            </a>
                            <a href="#warnings" class="btn btn-default btn-lg btn-warning" style="margin-right:15px;">
                                Warnings: <xsl:value-of select="count(//Report/IssueTypes/IssueType[@Severity='WARNING'])"/>
                            </a>
                            <a href="#hints" class="btn btn-default btn-lg btn-info" style="margin-right:15px;">
                                Hints: <xsl:value-of select="count(//Report/IssueTypes/IssueType[@Severity='HINT'])"/>
                            </a>
                            <a href="#suggestions" class="btn btn-default btn-lg btn-success" style="margin-right:15px;">
                                Suggestions: <xsl:value-of select="count(//Report/IssueTypes/IssueType[@Severity='SUGGESTION'])"/>
                            </a>
                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                    
                    <xsl:choose>
                        <xsl:when test="/Report/IssueTypes/IssueType[@Severity='ERROR']">
                            <a name="errors"></a>
                            <h2>
                                <span style="color:#d9534f;">| </span>Errors
                            </h2>

                            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='ERROR']">
                                <h3>
                                    <xsl:if test="@WikiUrl">
                                        <xsl:element name="a">
                                            <xsl:attribute name="href">
                                                <xsl:value-of select="@WikiUrl"/>
                                            </xsl:attribute>
                                            <xsl:value-of select="@Description"/>
                                            (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                        </xsl:element>
                                    </xsl:if>
                                    <xsl:if test="not(@WikiUrl)">
                                        <xsl:value-of select="@Description"/>
                                        (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                    </xsl:if>
                                </h3>
                                <table class="table table-striped table-bordered table-hover table-condensed">
                                    <tr class="error">
                                        <th>#</th>
                                        <th>File</th>
                                        <th>Line</th>
                                        <th>Message</th>
                                    </tr>
                                    <xsl:for-each select="key('ISSUETYPES',@Id)">
                                        <tr>
                                            <td>
                                                <xsl:value-of select="position()"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@File"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Line"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Message"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                </table>
                                <br />
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <!--No Errors where generated by InspectCode.-->
                        </xsl:otherwise>
                    </xsl:choose>

                    <xsl:choose>
                        <xsl:when test="/Report/IssueTypes/IssueType[@Severity='WARNING']">
                            <a name="warnings"></a>

                            <h2>
                                <span style="color:#f0ad4e;">| </span>Warnings
                            </h2>
                            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='WARNING']">
                                <h3>
                                    <xsl:if test="@WikiUrl">
                                        <xsl:element name="a">
                                            <xsl:attribute name="href">
                                                <xsl:value-of select="@WikiUrl"/>
                                            </xsl:attribute>
                                            <xsl:value-of select="@Description"/>
                                            (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                        </xsl:element>
                                    </xsl:if>
                                    <xsl:if test="not(@WikiUrl)">
                                        <xsl:value-of select="@Description"/>
                                        (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                    </xsl:if>
                                </h3>
                                <table class="table table-striped table-hover table-condensed">
                                    <tr>
                                        <th>#</th>
                                        <th>File</th>
                                        <th>Line</th>
                                        <th>Message</th>
                                    </tr>
                                    <xsl:for-each select="key('ISSUETYPES',@Id)">
                                        <tr>
                                            <td>
                                                <xsl:value-of select="position()"/>
                                            </td>

                                            <td>
                                                <xsl:value-of select="@File"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Line"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Message"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                </table>
                                <br />
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <!--No Warnings where generated by InspectCode.-->
                        </xsl:otherwise>
                    </xsl:choose>

                    <xsl:choose>
                        <xsl:when test="/Report/IssueTypes/IssueType[@Severity='SUGGESTION']">
                            <a name="suggestions"></a>
                            <h2>
                                <span style="color:#5cb85c;">| </span>Suggestions
                            </h2>

                            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='SUGGESTION']">
                                <h3>
                                    <xsl:if test="@WikiUrl">
                                        <xsl:element name="a">
                                            <xsl:attribute name="href">
                                                <xsl:value-of select="@WikiUrl"/>
                                            </xsl:attribute>
                                            <xsl:value-of select="@Description"/>
                                            (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                        </xsl:element>
                                    </xsl:if>
                                    <xsl:if test="not(@WikiUrl)">
                                        <xsl:value-of select="@Description"/>
                                        (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                    </xsl:if>
                                </h3>
                                <table class="table table-striped table-bordered table-hover table-condensed">
                                    <tr class="suggestion">
                                        <th>#</th>
                                        <th>File</th>
                                        <th>Line</th>
                                        <th>Message</th>
                                    </tr>
                                    <xsl:for-each select="key('ISSUETYPES',@Id)">
                                        <tr>
                                            <td>
                                                <xsl:value-of select="position()"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@File"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Line"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Message"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                </table>
                                <br />
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <!--No Suggestions where generated by InspectCode.-->
                        </xsl:otherwise>
                    </xsl:choose>

                    <xsl:choose>
                        <xsl:when test="/Report/IssueTypes/IssueType[@Severity='HINT']">
                            <a name="hints"></a>
                            <h2>
                                <span style="color:#5bc0de;">| </span>Hints
                            </h2>

                            <xsl:for-each select="/Report/IssueTypes/IssueType[@Severity='HINT']">
                                <h3>
                                    <xsl:if test="@WikiUrl">
                                        <xsl:element name="a">
                                            <xsl:attribute name="href">
                                                <xsl:value-of select="@WikiUrl"/>
                                            </xsl:attribute>
                                            <xsl:value-of select="@Description"/>
                                            (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                        </xsl:element>
                                    </xsl:if>
                                    <xsl:if test="not(@WikiUrl)">
                                        <xsl:value-of select="@Description"/>
                                        (<xsl:value-of select="count(key('ISSUETYPES',@Id))"/>)
                                    </xsl:if>
                                </h3>
                                <table class="table table-striped table-bordered table-hover table-condensed">
                                    <tr class="hint">
                                        <th>#</th>
                                        <th>File</th>
                                        <th>Line</th>
                                        <th>Message</th>
                                    </tr>
                                    <xsl:for-each select="key('ISSUETYPES',@Id)">
                                        <tr>
                                            <td>
                                                <xsl:value-of select="position()"/>
                                            </td>

                                            <td>
                                                <xsl:value-of select="@File"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Line"/>
                                            </td>
                                            <td>
                                                <xsl:value-of select="@Message"/>
                                            </td>
                                        </tr>
                                    </xsl:for-each>
                                </table>
                                <br />
                            </xsl:for-each>
                        </xsl:when>
                        <xsl:otherwise>
                            <!--No Hints where generated by InspectCode.-->
                        </xsl:otherwise>
                    </xsl:choose>
                </div>
                <span id="top-link-block" >
                    <a href="#top" class="well well-sm">
                        <i class="glyphicon glyphicon-chevron-up"></i> Back to Top
                    </a>
                </span>
                <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.1.3.min.js"></script>
                <script type="text/javascript" src="https://netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
                <script>
                    $('#top-link-block').removeClass('hidden').affix({
                        // how far to scroll down before link "slides" into view
                        offset: {top:100}
                    });
                </script>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>