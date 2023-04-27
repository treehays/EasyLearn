﻿using EasyLearn.Models.DTOs.EmailSenderDTOs;

namespace EasyLearn.GateWays.Email;

public static class EmailTemplates
{
    public static string ConfirmationEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        var confirmEmailTemplate = $"working";
        //var confirmEmailTemplate = $$""""

        //                <!DOCTYPE html
        //        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
        //    <html xmlns="http://www.w3.org/1999/xhtml" xmlns:o="urn:schemas-microsoft-com:office:office"
        //        style="font-family:arial, 'helvetica neue', helvetica, sans-serif">

        //    <head>
        //        <meta charset="UTF-8">
        //        <meta content="width=device-width, initial-scale=1" name="viewport">
        //        <meta name="x-apple-disable-message-reformatting">
        //        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        //        <meta content="telephone=no" name="format-detection">
        //        <title>New message</title><!--[if (mso 16)]>
        //                    <style type="text/css">
        //                    a {text-decoration: none;}
        //                    </style>
        //                    <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>
        //                <xml>
        //                    <o:OfficeDocumentSettings>
        //                    <o:AllowPNG></o:AllowPNG>
        //                    <o:PixelsPerInch>96</o:PixelsPerInch>
        //                    </o:OfficeDocumentSettings>
        //                </xml>
        //                <![endif]--><!--[if !mso]><!-- -->
        //        <link href="https://fonts.googleapis.com/css2?family=Imprima&display=swap" rel="stylesheet"><!--<![endif]-->
        //        <style type="text/css">
        //            #outlook a {
        //                padding: 0;
        //            }

        //            .es-button {
        //                mso-style-priority: 100 !important;
        //                text-decoration: none !important;
        //            }

        //            a[x-apple-data-detectors] {
        //                color: inherit !important;
        //                text-decoration: none !important;
        //                font-size: inherit !important;
        //                font-family: inherit !important;
        //                font-weight: inherit !important;
        //                line-height: inherit !important;
        //            }

        //            .es-desk-hidden {
        //                display: none;
        //                float: left;
        //                overflow: hidden;
        //                width: 0;
        //                max-height: 0;
        //                line-height: 0;
        //                mso-hide: all;
        //            }

        //            @media only screen and (max-width:600px) {

        //                p,
        //                ul li,
        //                ol li,
        //                a {
        //                    line-height: 150% !important
        //                }

        //                h1,
        //                h2,
        //                h3,
        //                h1 a,
        //                h2 a,
        //                h3 a {
        //                    line-height: 120%
        //                }

        //                h1 {
        //                    font-size: 30px !important;
        //                    text-align: left
        //                }

        //                h2 {
        //                    font-size: 24px !important;
        //                    text-align: left
        //                }

        //                h3 {
        //                    font-size: 20px !important;
        //                    text-align: left
        //                }

        //                .es-header-body h1 a,
        //                .es-content-body h1 a,
        //                .es-footer-body h1 a {
        //                    font-size: 30px !important;
        //                    text-align: left
        //                }

        //                .es-header-body h2 a,
        //                .es-content-body h2 a,
        //                .es-footer-body h2 a {
        //                    font-size: 24px !important;
        //                    text-align: left
        //                }

        //                .es-header-body h3 a,
        //                .es-content-body h3 a,
        //                .es-footer-body h3 a {
        //                    font-size: 20px !important;
        //                    text-align: left
        //                }

        //                .es-menu td a {
        //                    font-size: 14px !important
        //                }

        //                .es-header-body p,
        //                .es-header-body ul li,
        //                .es-header-body ol li,
        //                .es-header-body a {
        //                    font-size: 14px !important
        //                }

        //                .es-content-body p,
        //                .es-content-body ul li,
        //                .es-content-body ol li,
        //                .es-content-body a {
        //                    font-size: 14px !important
        //                }

        //                .es-footer-body p,
        //                .es-footer-body ul li,
        //                .es-footer-body ol li,
        //                .es-footer-body a {
        //                    font-size: 14px !important
        //                }

        //                .es-infoblock p,
        //                .es-infoblock ul li,
        //                .es-infoblock ol li,
        //                .es-infoblock a {
        //                    font-size: 12px !important
        //                }

        //                *[class="gmail-fix"] {
        //                    display: none !important
        //                }

        //                .es-m-txt-c,
        //                .es-m-txt-c h1,
        //                .es-m-txt-c h2,
        //                .es-m-txt-c h3 {
        //                    text-align: center !important
        //                }

        //                .es-m-txt-r,
        //                .es-m-txt-r h1,
        //                .es-m-txt-r h2,
        //                .es-m-txt-r h3 {
        //                    text-align: right !important
        //                }

        //                .es-m-txt-l,
        //                .es-m-txt-l h1,
        //                .es-m-txt-l h2,
        //                .es-m-txt-l h3 {
        //                    text-align: left !important
        //                }

        //                .es-m-txt-r img,
        //                .es-m-txt-c img,
        //                .es-m-txt-l img {
        //                    display: inline !important
        //                }

        //                .es-button-border {
        //                    display: block !important
        //                }

        //                a.es-button,
        //                button.es-button {
        //                    font-size: 18px !important;
        //                    display: block !important;
        //                    border-right-width: 0px !important;
        //                    border-left-width: 0px !important;
        //                    border-top-width: 15px !important;
        //                    border-bottom-width: 15px !important
        //                }

        //                .es-adaptive table,
        //                .es-left,
        //                .es-right {
        //                    width: 100% !important
        //                }

        //                .es-content table,
        //                .es-header table,
        //                .es-footer table,
        //                .es-content,
        //                .es-footer,
        //                .es-header {
        //                    width: 100% !important;
        //                    max-width: 600px !important
        //                }

        //                .es-adapt-td {
        //                    display: block !important;
        //                    width: 100% !important
        //                }

        //                .adapt-img {
        //                    width: 100% !important;
        //                    height: auto !important
        //                }

        //                .es-m-p0 {
        //                    padding: 0px !important
        //                }

        //                .es-m-p0r {
        //                    padding-right: 0px !important
        //                }

        //                .es-m-p0l {
        //                    padding-left: 0px !important
        //                }

        //                .es-m-p0t {
        //                    padding-top: 0px !important
        //                }

        //                .es-m-p0b {
        //                    padding-bottom: 0 !important
        //                }

        //                .es-m-p20b {
        //                    padding-bottom: 20px !important
        //                }

        //                .es-mobile-hidden,
        //                .es-hidden {
        //                    display: none !important
        //                }

        //                tr.es-desk-hidden,
        //                td.es-desk-hidden,
        //                table.es-desk-hidden {
        //                    width: auto !important;
        //                    overflow: visible !important;
        //                    float: none !important;
        //                    max-height: inherit !important;
        //                    line-height: inherit !important
        //                }

        //                tr.es-desk-hidden {
        //                    display: table-row !important
        //                }

        //                table.es-desk-hidden {
        //                    display: table !important
        //                }

        //                td.es-desk-menu-hidden {
        //                    display: table-cell !important
        //                }

        //                .es-menu td {
        //                    width: 1% !important
        //                }

        //                table.es-table-not-adapt,
        //                .esd-block-html table {
        //                    width: auto !important
        //                }

        //                table.es-social {
        //                    display: inline-block !important
        //                }

        //                table.es-social td {
        //                    display: inline-block !important
        //                }

        //                .es-desk-hidden {
        //                    display: table-row !important;
        //                    width: auto !important;
        //                    overflow: visible !important;
        //                    max-height: inherit !important
        //                }
        //            }
        //        </style>
        //    </head>

        //    <body
        //        style="width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0">
        //        <div class="es-wrapper-color" style="background-color:#FFFFFF"><!--[if gte mso 9]>
        //                			<v:background xmlns:v="urn:schemas-microsoft-com:vml" fill="t">
        //                				<v:fill type="tile" color="#ffffff"></v:fill>
        //                			</v:background>
        //                		<![endif]-->
        //            <table class="es-wrapper" width="100%" cellspacing="0" cellpadding="0"
        //                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FFFFFF">
        //                <tr>
        //                    <td valign="top" style="padding:0;Margin:0">
        //                        <table cellpadding="0" cellspacing="0" class="es-footer" align="center"
        //                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top">
        //                            <tr>
        //                                <td align="center" style="padding:0;Margin:0">
        //                                    <table bgcolor="#bcb8b1" class="es-footer-body" align="center" cellpadding="0"
        //                                        cellspacing="0"
        //                                        style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px">
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="Margin:0;padding-top:20px;padding-bottom:20px;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="center" valign="top"
        //                                                            style="padding:0;Margin:0;width:520px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                role="presentation"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                <tr>
        //                                                                    <td align="center"
        //                                                                        style="padding:0;Margin:0;font-size:0px"><a
        //                                                                            target="_blank" href="{{{baseUrl}}}/4"
        //                                                                            style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:14px"><img
        //                                                                                src="https://gupxey.stripocdn.email/content/guids/CABINET_055ba03e85e991c70304fecd78a2dceaf6b3f0bc1b0eb49336463d3599679494/images/vector.png"
        //                                                                                alt="Logo"
        //                                                                                style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"
        //                                                                                height="60" title="Logo"></a></td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                    </table>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                        <table cellpadding="0" cellspacing="0" class="es-content" align="center"
        //                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%">
        //                            <tr>
        //                                <td align="center" style="padding:0;Margin:0">
        //                                    <table bgcolor="#efefef" class="es-content-body" align="center" cellpadding="0"
        //                                        cellspacing="0"
        //                                        style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#EFEFEF;border-radius:20px 20px 0 0;width:600px">
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="padding:0;Margin:0;padding-top:40px;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="center" valign="top"
        //                                                            style="padding:0;Margin:0;width:520px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                role="presentation"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                <tr>
        //                                                                    <td align="left" class="es-m-txt-c"
        //                                                                        style="padding:0;Margin:0;font-size:0px"><a
        //                                                                            target="_blank" href="{{baseUrl}}/1"
        //                                                                            style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:18px"><img
        //                                                                                src="https://gupxey.stripocdn.email/content/guids/CABINET_ee77850a5a9f3068d9355050e69c76d26d58c3ea2927fa145f0d7a894e624758/images/group_4076323.png"
        //                                                                                alt="Confirm email"
        //                                                                                style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;border-radius:100px"
        //                                                                                width="100" title="Confirm email"></a></td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="padding:0;Margin:0;padding-top:20px;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="center" valign="top"
        //                                                            style="padding:0;Margin:0;width:520px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                bgcolor="#fafafa"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;background-color:#fafafa;border-radius:10px"
        //                                                                role="presentation">
        //                                                                <tr>
        //                                                                    <td align="left" style="padding:20px;Margin:0">
        //                                                                        <h3
        //                                                                            style="Margin:0;line-height:34px;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;font-size:28px;font-style:normal;font-weight:bold;color:#2D3142">
        //                                                                            Welcome,&nbsp;{{model.ReceiverName}}
        //                                                                        </h3>
        //                                                                        <p
        //                                                                            style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:27px;color:#2D3142;font-size:18px">
        //                                                                            <br></p>
        //                                                                        <p
        //                                                                            style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:27px;color:#2D3142;font-size:18px">
        //                                                                            You're receiving this message because you
        //                                                                            recently signed up for an
        //                                                                            account.<br><br>Confirm your email address by
        //                                                                            clicking the button below. This step adds extra
        //                                                                            security to your business by verifying you own
        //                                                                            this email.</p>
        //                                                                    </td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                    </table>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                        <table cellpadding="0" cellspacing="0" class="es-content" align="center"
        //                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%">
        //                            <tr>
        //                                <td align="center" style="padding:0;Margin:0">
        //                                    <table bgcolor="#efefef" class="es-content-body" align="center" cellpadding="0"
        //                                        cellspacing="0"
        //                                        style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#EFEFEF;width:600px">
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="Margin:0;padding-top:30px;padding-bottom:40px;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="center" valign="top"
        //                                                            style="padding:0;Margin:0;width:520px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                role="presentation"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                <tr>
        //                                                                    <td align="center" style="padding:0;Margin:0"><!--[if mso]><a href="{{baseUrl}}/2" target="_blank" hidden>
        //                	<v:roundrect xmlns:v="urn:schemas-microsoft-com:vml" xmlns:w="urn:schemas-microsoft-com:office:word" esdevVmlButton href="{{baseUrl}}/3" 
        //                                style="height:56px; v-text-anchor:middle; width:520px" arcsize="50%" stroke="f"  fillcolor="#7630f3">
        //                		<w:anchorlock></w:anchorlock>
        //                		<center style='color:#ffffff; font-family:Imprima, Arial, sans-serif; font-size:22px; font-weight:700; line-height:22px;  mso-text-raise:1px'>Confirm email</center>
        //                	</v:roundrect></a>
        //                <![endif]--><!--[if !mso]><!-- --><span class="msohide es-button-border"
        //                                                                            style="border-style:solid;border-color:#2CB543;background:#7630f3;border-width:0px;display:block;border-radius:30px;width:auto;mso-border-alt:10px;mso-hide:all"><a
        //                                                                                href="{{model.EmailToken}}"
        //                                                                                class="es-button msohide" target="_blank"
        //                                                                                style="mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:22px;padding:15px 20px 15px 20px;display:block;background:#7630f3;border-radius:30px;font-family:Imprima, Arial, sans-serif;font-weight:bold;font-style:normal;line-height:26px;width:auto;text-align:center;mso-hide:all;padding-left:5px;padding-right:5px;border-color:#7630f3">Confirm
        //                                                                                email</a></span><!--<![endif]--></td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="padding:0;Margin:0;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="center" valign="top"
        //                                                            style="padding:0;Margin:0;width:520px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                role="presentation"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                <tr>
        //                                                                    <td align="left" style="padding:0;Margin:0">
        //                                                                        <p
        //                                                                            style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:27px;color:#2D3142;font-size:18px">
        //                                                                            Thanks,<br><br>EasyLearn CEO</p>
        //                                                                    </td>
        //                                                                </tr>
        //                                                                <tr>
        //                                                                    <td align="center"
        //                                                                        style="padding:0;Margin:0;padding-bottom:20px;padding-top:40px;font-size:0">
        //                                                                        <table border="0" width="100%" height="100%"
        //                                                                            cellpadding="0" cellspacing="0"
        //                                                                            role="presentation"
        //                                                                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                            <tr>
        //                                                                                <td
        //                                                                                    style="padding:0;Margin:0;border-bottom:1px solid #666666;background:unset;height:1px;width:100%;margin:0px">
        //                                                                                </td>
        //                                                                            </tr>
        //                                                                        </table>
        //                                                                    </td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                    </table>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                        <table cellpadding="0" cellspacing="0" class="es-content" align="center"
        //                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%">
        //                            <tr>
        //                                <td align="center" style="padding:0;Margin:0">
        //                                    <table bgcolor="#efefef" class="es-content-body" align="center" cellpadding="0"
        //                                        cellspacing="0"
        //                                        style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#EFEFEF;border-radius:0 0 20px 20px;width:600px">
        //                                        <tr>
        //                                            <td class="esdev-adapt-off" align="left"
        //                                                style="Margin:0;padding-top:20px;padding-bottom:20px;padding-left:40px;padding-right:40px">
        //                                                <table cellpadding="0" cellspacing="0" class="esdev-mso-table"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:520px">
        //                                                    <tr>
        //                                                        <td class="esdev-mso-td" valign="top" style="padding:0;Margin:0">
        //                                                            <table cellpadding="0" cellspacing="0" align="left"
        //                                                                class="es-left"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left">
        //                                                                <tr>
        //                                                                    <td align="center" valign="top"
        //                                                                        style="padding:0;Margin:0;width:47px">
        //                                                                        <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                            role="presentation"
        //                                                                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                            <tr>
        //                                                                                <td align="center" class="es-m-txt-l"
        //                                                                                    style="padding:0;Margin:0;font-size:0px">
        //                                                                                    <a target="_blank"
        //                                                                                        href="{{baseUrl}}/5"
        //                                                                                        style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:18px"><img
        //                                                                                            src="https://gupxey.stripocdn.email/content/guids/CABINET_ee77850a5a9f3068d9355050e69c76d26d58c3ea2927fa145f0d7a894e624758/images/group_4076325.png"
        //                                                                                            alt="Demo"
        //                                                                                            style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"
        //                                                                                            width="47" title="Demo"></a>
        //                                                                                </td>
        //                                                                            </tr>
        //                                                                        </table>
        //                                                                    </td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                        <td style="padding:0;Margin:0;width:20px"></td>
        //                                                        <td class="esdev-mso-td" valign="top" style="padding:0;Margin:0">
        //                                                            <table cellpadding="0" cellspacing="0" class="es-right"
        //                                                                align="right"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right">
        //                                                                <tr>
        //                                                                    <td align="center" valign="top"
        //                                                                        style="padding:0;Margin:0;width:453px">
        //                                                                        <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                            role="presentation"
        //                                                                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                            <tr>
        //                                                                                <td align="left" style="padding:0;Margin:0">
        //                                                                                    <p
        //                                                                                        style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:24px;color:#2D3142;font-size:16px">
        //                                                                                        This link expire in 24 hours {{DateTime.Now.AddHours(24)}}. If you
        //                                                                                        have questions, <a target="_blank"
        //                                                                                            style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:16px"
        //                                                                                            href="{{baseUrl}}/6">we're
        //                                                                                            here to help</a></p>
        //                                                                                </td>
        //                                                                            </tr>
        //                                                                        </table>
        //                                                                    </td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                    </table>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                        <table cellpadding="0" cellspacing="0" class="es-footer" align="center"
        //                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top">
        //                            <tr>
        //                                <td align="center" style="padding:0;Margin:0">
        //                                    <table bgcolor="#bcb8b1" class="es-footer-body" align="center" cellpadding="0"
        //                                        cellspacing="0"
        //                                        style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px">
        //                                        <tr>
        //                                            <td align="left"
        //                                                style="Margin:0;padding-left:20px;padding-right:20px;padding-bottom:30px;padding-top:40px">
        //                                                <table cellpadding="0" cellspacing="0" width="100%"
        //                                                    style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                    <tr>
        //                                                        <td align="left" style="padding:0;Margin:0;width:560px">
        //                                                            <table cellpadding="0" cellspacing="0" width="100%"
        //                                                                role="presentation"
        //                                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                <tr>
        //                                                                    <td align="center" class="es-m-txt-c"
        //                                                                        style="padding:0;Margin:0;padding-bottom:20px;font-size:0px">
        //                                                                        <img src="https://gupxey.stripocdn.email/content/guids/CABINET_055ba03e85e991c70304fecd78a2dceaf6b3f0bc1b0eb49336463d3599679494/images/vector.png"
        //                                                                            alt="Logo"
        //                                                                            style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;font-size:12px"
        //                                                                            title="Logo" height="60"></td>
        //                                                                </tr>
        //                                                                <tr>
        //                                                                    <td align="center" class="es-m-txt-c"
        //                                                                        style="padding:0;Margin:0;padding-top:10px;padding-bottom:20px;font-size:0">
        //                                                                        <table cellpadding="0" cellspacing="0"
        //                                                                            class="es-table-not-adapt es-social"
        //                                                                            role="presentation"
        //                                                                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                                            <tr>
        //                                                                                <td align="center" valign="top"
        //                                                                                    style="padding:0;Margin:0;padding-right:5px">
        //                                                                                    <img src="https://gupxey.stripocdn.email/content/assets/img/social-icons/logo-black/twitter-logo-black.png"
        //                                                                                        alt="Tw" title="Twitter" height="24"
        //                                                                                        style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic">
        //                                                                                </td>
        //                                                                                <td align="center" valign="top"
        //                                                                                    style="padding:0;Margin:0;padding-right:5px">
        //                                                                                    <img src="https://gupxey.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png"
        //                                                                                        alt="Fb" title="Facebook"
        //                                                                                        height="24"
        //                                                                                        style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic">
        //                                                                                </td>
        //                                                                                <td align="center" valign="top"
        //                                                                                    style="padding:0;Margin:0"><img
        //                                                                                        src="https://gupxey.stripocdn.email/content/assets/img/social-icons/logo-black/linkedin-logo-black.png"
        //                                                                                        alt="In" title="Linkedin"
        //                                                                                        height="24"
        //                                                                                        style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic">
        //                                                                                </td>
        //                                                                            </tr>
        //                                                                        </table>
        //                                                                    </td>
        //                                                                </tr>
        //                                                                <tr>
        //                                                                    <td align="center" style="padding:0;Margin:0">
        //                                                                        <p
        //                                                                            style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:20px;color:#2D3142;font-size:13px">
        //                                                                            <a target="_blank"
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;color:#2D3142;font-size:14px"
        //                                                                                href=""></a><a target="_blank"
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;color:#2D3142;font-size:14px"
        //                                                                                href="">Privacy Policy</a><a target="_blank"
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;color:#2D3142;font-size:13px"
        //                                                                                href=""></a> • <a target="_blank"
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;color:#2D3142;font-size:14px"
        //                                                                                href="">Unsubscribe</a></p>
        //                                                                    </td>
        //                                                                </tr>
        //                                                                <tr>
        //                                                                    <td align="center"
        //                                                                        style="padding:0;Margin:0;padding-top:20px">
        //                                                                        <p
        //                                                                            style="Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:Imprima, Arial, sans-serif;line-height:21px;color:#2D3142;font-size:14px">
        //                                                                            <a target="_blank" href=""
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:14px"></a>Copyright
        //                                                                            © 2023&nbsp;Company<a target="_blank" href=""
        //                                                                                style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#2D3142;font-size:14px"></a>
        //                                                                        </p>
        //                                                                    </td>
        //                                                                </tr>
        //                                                            </table>
        //                                                        </td>
        //                                                    </tr>
        //                                                </table>
        //                                            </td>
        //                                        </tr>
        //                                    </table>
        //                                </td>
        //                            </tr>
        //                        </table>
        //                    </td>
        //                </tr>
        //            </table>
        //        </div>
        //    </body>

        //    </html>

        //    """";
        return confirmEmailTemplate;
        //return null;
    }

    public static string EnrollmentEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        var confirmEmailTemplate = $"working";
        //var confirmEmailTemplate = $$""""
        // everything here
        //    """";

        return confirmEmailTemplate;
        return confirmEmailTemplate;


    }

    public static string CourseCompletionEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        var confirmEmailTemplate = $"working";
        //var confirmEmailTemplate = $$""""
        // everything here
        //    """";

        return confirmEmailTemplate;


    }

    public static string WithdrawalComfirmationEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        var confirmEmailTemplate = $"working";
        //var confirmEmailTemplate = $$""""
        // everything here
        //    """";

        return confirmEmailTemplate;


    }

    public static string PasswordRessetEmailTemplate(EmailSenderDetails model, string baseUrl)
    {
        var confirmEmailTemplate = $"working";
        //var confirmEmailTemplate = $$""""
        // <!DOCTYPE html
        //        PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >
        //    < html xmlns = "http://www.w3.org/1999/xhtml" xmlns: o = "urn:schemas-microsoft-com:office:office"
        //    style = "width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0" >

        //< head >
        //    < meta charset = "UTF-8" >
        //    < meta content = "width=device-width, initial-scale=1" name = "viewport" >
        //    < meta name = "x-apple-disable-message-reformatting" >
        //    < meta http - equiv = "X-UA-Compatible" content = "IE=edge" >
        //    < meta content = "telephone=no" name = "format-detection" >
        //    < title > Trigger newsletter 5 </ title >< !--[if (mso 16)]>
        //< style type = "text/css" >
        //a { text - decoration: none; }
        //</ style >
        //< ![endif]-- >< !--[if gte mso 9]>< style > sup { font - size: 100 % !important; }</ style >< ![endif]-- >< !--[if gte mso 9]>
        //< xml >
        //< o:OfficeDocumentSettings >
        //< o:AllowPNG ></ o:AllowPNG >
        //< o:PixelsPerInch > 96 </ o:PixelsPerInch >
        //</ o:OfficeDocumentSettings >
        //</ xml >
        //< ![endif]-- >< !--[if !mso]>< !---->
        //    < link href = "https://fonts.googleapis.com/css?family=Lato:400,400i,700,700i" rel = "stylesheet" >< !--< ![endif]-- >
        //    < style type = "text/css" >
        //# outlook a {
        //            padding: 0;
        //    }

        //        .ExternalClass {
        //            width: 100%;
        //        }

        //        .ExternalClass,
        //        .ExternalClass p,
        //        .ExternalClass span,
        //        .ExternalClass font,
        //        .ExternalClass td,
        //        .ExternalClass div
        //{
        //    line-height: 100%;
        //}

        //        .es-button {
        //            mso-style-priority: 100 !important;
        //            text-decoration: none !important;
        //        }

        //        a[x - apple - data - detectors] {
        //color: inherit!important;
        //    text - decoration: none!important;
        //    font - size: inherit!important;
        //    font - family: inherit!important;
        //    font - weight: inherit!important;
        //    line - height: inherit!important;
        //}

        //        .es - desk - hidden {
        //display: none;
        //    float: left;
        //overflow: hidden;
        //width: 0;
        //    max - height: 0;
        //    line - height: 0;
        //    mso - hide: all;
        //}

        //@media only screen and (max-width:600px) {

        //    p,
        //            ul li,
        //            ol li,
        //            a {
        //        line - height: 150 % !important
        //            }

        //    h1,
        //            h2,
        //            h3,
        //            h1 a,
        //            h2 a,
        //            h3 a {
        //        line - height: 120 % !important
        //            }

        //    h1 {
        //        font - size: 30px!important;
        //        text - align: center
        //            }

        //    h2 {
        //        font - size: 26px!important;
        //        text - align: center
        //            }

        //    h3 {
        //        font - size: 20px!important;
        //        text - align: center
        //            }

        //            .es - header - body h1 a,
        //            .es - content - body h1 a,
        //            .es - footer - body h1 a {
        //        font - size: 30px!important
        //            }

        //            .es - header - body h2 a,
        //            .es - content - body h2 a,
        //            .es - footer - body h2 a {
        //        font - size: 26px!important
        //            }

        //            .es - header - body h3 a,
        //            .es - content - body h3 a,
        //            .es - footer - body h3 a {
        //        font - size: 20px!important
        //            }

        //            .es - menu td a {
        //        font - size: 16px!important
        //            }

        //            .es - header - body p,
        //            .es - header - body ul li,
        //            .es - header - body ol li,
        //            .es - header - body a {
        //        font - size: 16px!important
        //            }

        //            .es - content - body p,
        //            .es - content - body ul li,
        //            .es - content - body ol li,
        //            .es - content - body a {
        //        font - size: 16px!important
        //            }

        //            .es - footer - body p,
        //            .es - footer - body ul li,
        //            .es - footer - body ol li,
        //            .es - footer - body a {
        //        font - size: 16px!important
        //            }

        //            .es - infoblock p,
        //            .es - infoblock ul li,
        //            .es - infoblock ol li,
        //            .es - infoblock a {
        //        font - size: 12px!important
        //            }

        //    *[class= "gmail-fix"] {
        //display: none!important
        //            }

        //            .es - m - txt - c,
        //            .es - m - txt - c h1,
        //            .es - m - txt - c h2,
        //            .es - m - txt - c h3
        //{
        //    text - align: center!important
        //            }

        //            .es - m - txt - r,
        //            .es - m - txt - r h1,
        //            .es - m - txt - r h2,
        //            .es - m - txt - r h3
        //{
        //    text - align: right!important
        //            }

        //            .es - m - txt - l,
        //            .es - m - txt - l h1,
        //            .es - m - txt - l h2,
        //            .es - m - txt - l h3
        //{
        //    text - align: left!important
        //            }

        //            .es - m - txt - r img,
        //            .es - m - txt - c img,
        //            .es - m - txt - l img
        //{
        //display: inline!important
        //            }

        //            .es - button - border {
        //display: block!important
        //            }

        //a.es - button,
        //            button.es - button {
        //    font - size: 20px!important;
        //display: block!important;
        //padding: 15px 25px 15px 25px!important
        //            }

        //            .es - btn - fw {
        //    border - width: 10px 0px!important;
        //    text - align: center!important
        //            }

        //            .es - adaptive table,
        //            .es - btn - fw,
        //            .es - btn - fw - brdr,
        //            .es - left,
        //            .es - right {
        //width: 100 % !important
        //            }

        //            .es - content table,
        //            .es - header table,
        //            .es - footer table,
        //            .es - content,
        //            .es - footer,
        //            .es - header {
        //width: 100 % !important;
        //    max - width: 600px!important
        //            }

        //            .es - adapt - td {
        //display: block!important;
        //width: 100 % !important
        //            }

        //            .adapt - img {
        //width: 100 % !important;
        //height: auto!important
        //            }

        //            .es - m - p0 {
        //padding: 0px!important
        //            }

        //            .es - m - p0r {
        //    padding - right: 0px!important
        //            }

        //            .es - m - p0l {
        //    padding - left: 0px!important
        //            }

        //            .es - m - p0t {
        //    padding - top: 0px!important
        //            }

        //            .es - m - p0b {
        //    padding - bottom: 0!important
        //            }

        //            .es - m - p20b {
        //    padding - bottom: 20px!important
        //            }

        //            .es - mobile - hidden,
        //            .es - hidden {
        //display: none!important
        //            }

        //tr.es - desk - hidden,
        //            td.es - desk - hidden,
        //            table.es - desk - hidden {
        //width: auto!important;
        //overflow: visible!important;
        //    float: none!important;
        //    max - height: inherit!important;
        //    line - height: inherit!important
        //            }

        //tr.es - desk - hidden {
        //display: table - row!important
        //            }

        //table.es - desk - hidden {
        //display: table!important
        //            }

        //td.es - desk - menu - hidden {
        //display: table - cell!important
        //            }

        //            .es - menu td
        //{
        //width: 1 % !important
        //            }

        //table.es - table - not - adapt,
        //            .esd - block - html table
        //{
        //width: auto!important
        //            }

        //table.es - social {
        //display: inline - block!important
        //            }

        //table.es - social td
        //{
        //display: inline - block!important
        //            }

        //            .es - desk - hidden {
        //display: table - row!important;
        //width: auto!important;
        //overflow: visible!important;
        //    max - height: inherit!important
        //            }
        //        }
        //    </ style >
        //</ head >

        //< body
        //    style = "width:100%;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;padding:0;Margin:0" >
        //    < div class= "es-wrapper-color" style = "background-color:#F4F4F4" >< !--[if gte mso 9]>
        //< v:background xmlns:v = "urn:schemas-microsoft-com:vml" fill = "t" >
        //< v:fill type = "tile" color="#f4f4f4"></v:fill >
        //</ v:background >
        //< ![endif]-- >
        //        < table class= "es-wrapper" width = "100%" cellspacing = "0" cellpadding = "0"
        //            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#F4F4F4" >
        //            < tr class= "gmail-fix" height = "0" style = "border-collapse:collapse" >
        //                < td style = "padding:0;Margin:0" >
        //                    < table cellspacing = "0" cellpadding = "0" border = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;width:600px" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td cellpadding = "0" cellspacing = "0" border = "0"
        //                                style = "padding:0;Margin:0;line-height:1px;min-width:600px" height = "0" >< img
        //                                    src = "https://gupxey.stripocdn.email/content/guids/CABINET_837dc1d79e3a5eca5eb1609bfe9fd374/images/41521605538834349.png"
        //                                    style = "display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;max-height:0px;min-height:0px;min-width:600px;width:600px"
        //                                    alt width = "600" height="1"></td>
        //                        </tr>
        //                    </table>
        //                </td>
        //            </tr>
        //            <tr style="border-collapse:collapse">
        //                <td valign="top" style="padding:0;Margin:0">
        //                    <table cellpadding="0" cellspacing="0" class= "es-content" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left"
        //                                            style = "Margin:0;padding-left:10px;padding-right:10px;padding-top:15px;padding-bottom:15px" >
        //                                            < !--[if mso]>< table style = "width:580px" cellpadding = "0" cellspacing = "0" >< tr >< td style = "width:282px" valign = "top" >< ![endif]-- >
        //                                            < table class= "es-left" cellspacing = "0" cellpadding = "0" align = "left"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:left" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td align = "left" style = "padding:0;Margin:0;width:282px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td class= "es-infoblock es-m-txt-c" align = "left"
        //                                                                    style = "padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica\ neue', helvetica, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px" >
        //                                                                        Put your preheader text here<br></p>
        //                                                                </td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                            <!--[if mso]></ td >< td style = "width:20px" ></ td >< td style = "width:278px" valign = "top" >< ![endif]-- >
        //                                            < table class= "es-right" cellspacing = "0" cellpadding = "0" align = "right"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;float:right" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td align = "left" style = "padding:0;Margin:0;width:278px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "right" class= "es-infoblock es-m-txt-c"
        //                                                                    style = "padding:0;Margin:0;line-height:14px;font-size:12px;color:#CCCCCC" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:14px;color:#CCCCCC;font-size:12px" >
        //                                                                        < a href = "https://viewstripo.email" class= "view"
        //                                                                            target = "_blank"
        //                                                                            style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px;font-family:arial, 'helvetica neue', helvetica, sans-serif" > View
        //                                                                            in browser </ a ></ p >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                        </ table >
        //                                                    </ td >
        //                                                </ tr >
        //                                            </ table >< !--[if mso]></ td ></ tr ></ table >< ![endif]-- >
        //                                        </ td >
        //                                    </ tr >
        //                                </ table >
        //                            </ td >
        //                        </ tr >
        //                    </ table >
        //                    < table class= "es-header" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:#7C72DC;background-repeat:repeat;background-position:center top" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td style = "padding:0;Margin:0;background-color:#7c72dc" bgcolor = "#7c72dc" align = "center" >
        //                                < table class= "es-header-body" cellspacing = "0" cellpadding = "0" align = "center"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#7C72DC;width:600px" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left"
        //                                            style = "Margin:0;padding-bottom:10px;padding-left:10px;padding-right:10px;padding-top:20px" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:580px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "center"
        //                                                                    style = "Margin:0;padding-left:10px;padding-right:10px;padding-top:25px;padding-bottom:25px;font-size:0" >
        //                                                                    < img src = "https://gupxey.stripocdn.email/content/guids/CABINET_3df254a10a99df5e44cb27b842c2c69e/images/7331519201751184.png"
        //                                                                        alt
        //                                                                        style = "display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"
        //                                                                        width="40"></td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </table>
        //                    <table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td style = "padding:0;Margin:0;background-color:#7c72dc" bgcolor = "#7c72dc" align = "center" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left" style = "padding:0;Margin:0" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:600px" >
        //                                                        < table
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;background-color:#ffffff;border-radius:4px"
        //                                                            width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            bgcolor = "#ffffff" role = "presentation" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "center"
        //                                                                    style = "Margin:0;padding-bottom:5px;padding-left:30px;padding-right:30px;padding-top:35px" >
        //                                                                    < h1
        //                                                                        style = "Margin:0;line-height:58px;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;font-size:48px;font-style:normal;font-weight:normal;color:#111111" >
        //                                                                        Trouble signing in?</ h1 >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td bgcolor = "#ffffff" align = "center"
        //                                                                    style = "Margin:0;padding-top:5px;padding-bottom:5px;padding-left:20px;padding-right:20px;font-size:0" >
        //                                                                    < table width = "100%" height = "100%" cellspacing = "0"
        //                                                                        cellpadding = "0" border = "0" role = "presentation"
        //                                                                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                                        < tr style = "border-collapse:collapse" >
        //                                                                            < td
        //                                                                                style = "padding:0;Margin:0;border-bottom:1px solid #ffffff;background:#FFFFFF none repeat scroll 0% 0%;height:1px;width:100%;margin:0px" >
        //                                                                            </ td >
        //                                                                        </ tr >
        //                                                                    </ table >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                        </ table >
        //                                                    </ td >
        //                                                </ tr >
        //                                            </ table >
        //                                        </ td >
        //                                    </ tr >
        //                                </ table >
        //                            </ td >
        //                        </ tr >
        //                    </ table >
        //                    < table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#ffffff;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" bgcolor = "#ffffff" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left" style = "padding:0;Margin:0" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:600px" >
        //                                                        < table
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#ffffff"
        //                                                            width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            bgcolor = "#ffffff" role = "presentation" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td class= "es-m-txt-l" bgcolor = "#ffffff" align = "left"
        //                                                                    style = "Margin:0;padding-bottom:15px;padding-top:20px;padding-left:30px;padding-right:30px" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:27px;color:#666666;font-size:18px" >
        //                                                                        Resetting your password is easy. Just press the
        //                                                                        button below and follow the instructions. We'll
        //                                                                        have you up and running in no time.</p>
        //                                                                </td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                    <tr style="border-collapse:collapse">
        //                                        <td align="left"
        //                                            style="padding:0;Margin:0;padding-bottom:20px;padding-left:30px;padding-right:30px">
        //                                            <table width="100%" cellspacing="0" cellpadding="0"
        //                                                style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                <tr style="border-collapse:collapse">
        //                                                    <td valign="top" align="center"
        //                                                        style="padding:0;Margin:0;width:540px">
        //                                                        <table width="100%" cellspacing="0" cellpadding="0"
        //                                                            role="presentation"
        //                                                            style="mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px">
        //                                                            <tr style="border-collapse:collapse">
        //                                                                <td align="center"
        //                                                                    style="Margin:0;padding-left:10px;padding-right:10px;padding-top:40px;padding-bottom:40px">
        //                                                                    <span class= "es-button-border"
        //                                                                        style = "border-style:solid;border-color:#7C72DC;background:#7C72DC;border-width:1px;display:inline-block;border-radius:2px;width:auto;mso-border-alt:10px" >< a
        //                                                                            href = "https://viewstripo.email/"
        //                                                                            class= "es-button" target = "_blank"
        //                                                                            style = "mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:20px;display:inline-block;background:#7C72DC;border-radius:2px;font-family:helvetica, 'helvetica neue', arial, verdana, sans-serif;font-weight:normal;font-style:normal;line-height:24px;width:auto;text-align:center;padding:15px 25px 15px 25px" > Reset
        //                                                                            Password </ a ></ span ></ td >
        //                                                            </ tr >
        //                                                        </ table >
        //                                                    </ td >
        //                                                </ tr >
        //                                            </ table >
        //                                        </ td >
        //                                    </ tr >
        //                                </ table >
        //                            </ td >
        //                        </ tr >
        //                    </ table >
        //                    < table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body" cellspacing = "0" cellpadding = "0" bgcolor = "#ffffff"
        //                                    align = "center"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left" style = "padding:0;Margin:0" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:600px" >
        //                                                        < table
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:4px;background-color:#111111"
        //                                                            width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            bgcolor = "#111111" role = "presentation" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td class= "es-m-txt-l" bgcolor = "#111111" align = "left"
        //                                                                    style = "padding:0;Margin:0;padding-left:30px;padding-right:30px;padding-top:35px" >
        //                                                                    < h2
        //                                                                        style = "Margin:0;line-height:29px;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;font-size:24px;font-style:normal;font-weight:normal;color:#ffffff" >
        //                                                                        Want a more secure account?<br></h2>
        //                                                                </td>
        //                                                            </tr>
        //                                                            <tr style="border-collapse:collapse">
        //                                                                <td class= "es-m-txt-l" align = "left"
        //                                                                    style = "padding:0;Margin:0;padding-top:20px;padding-left:30px;padding-right:30px" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:27px;color:#666666;font-size:18px" >
        //                                                                        We support two-factor authentication to help
        //                                                                        keep your information private.< br ></ p >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td esdev - links - color = "#7c72dc" align = "left"
        //                                                                    style = "Margin:0;padding-top:20px;padding-left:30px;padding-right:30px;padding-bottom:40px" >
        //                                                                    < a target = "_blank" href = "https://viewstripo.email/"
        //                                                                        style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#7c72dc;font-size:18px" > See
        //                                                                        how easy it is to get started</a></td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </table>
        //                    <table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left" style = "padding:0;Margin:0" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:600px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "center"
        //                                                                    style = "Margin:0;padding-top:10px;padding-bottom:20px;padding-left:20px;padding-right:20px;font-size:0" >
        //                                                                    < table width = "100%" height = "100%" cellspacing = "0"
        //                                                                        cellpadding = "0" border = "0" role = "presentation"
        //                                                                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                                        < tr style = "border-collapse:collapse" >
        //                                                                            < td
        //                                                                                style = "padding:0;Margin:0;border-bottom:1px solid #f4f4f4;background:#FFFFFF none repeat scroll 0% 0%;height:1px;width:100%;margin:0px" >
        //                                                                            </ td >
        //                                                                        </ tr >
        //                                                                    </ table >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                        </ table >
        //                                                    </ td >
        //                                                </ tr >
        //                                            </ table >
        //                                        </ td >
        //                                    </ tr >
        //                                </ table >
        //                            </ td >
        //                        </ tr >
        //                    </ table >
        //                    < table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#c6c2ed;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" bgcolor = "#c6c2ed" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left" style = "padding:0;Margin:0" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:600px" >
        //                                                        < table
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:4px"
        //                                                            width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "center"
        //                                                                    style = "padding:0;Margin:0;padding-top:30px;padding-left:30px;padding-right:30px" >
        //                                                                    < h3
        //                                                                        style = "Margin:0;line-height:24px;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;font-size:20px;font-style:normal;font-weight:normal;color:#111111" >
        //                                                                        Need more help?</h3>
        //                                                                </td>
        //                                                            </tr>
        //                                                            <tr style="border-collapse:collapse">
        //                                                                <td esdev-links-color="#7c72dc" align="center"
        //                                                                    style="padding:0;Margin:0;padding-bottom:30px;padding-left:30px;padding-right:30px">
        //                                                                    <a target="_blank" href="https://viewstripo.email/"
        //                                                                        style="-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#7c72dc;font-size:18px">We’re
        //                                                                        here, ready to talk</a></td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </table>
        //                    <table cellpadding="0" cellspacing="0" class= "es-footer" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-footer-body" cellspacing = "0" cellpadding = "0" align = "center"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left"
        //                                            style = "Margin:0;padding-top:30px;padding-bottom:30px;padding-left:30px;padding-right:30px" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:540px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "left" style = "padding:0;Margin:0" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:21px;color:#666666;font-size:14px" >
        //                                                                        < strong >< a target = "_blank"
        //                                                                                href = "https://viewstripo.email"
        //                                                                                style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#111111;font-size:14px" > Dashboard </ a >
        //                                                                            - < a target = "_blank"
        //                                                                                href = "https://viewstripo.email"
        //                                                                                style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#111111;font-size:14px" > Billing </ a >
        //                                                                            - < a target = "_blank"
        //                                                                                href = "https://viewstripo.email"
        //                                                                                style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#111111;font-size:14px" > Help </ a ></ strong >
        //                                                                    </ p >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "left"
        //                                                                    style = "padding:0;Margin:0;padding-top:25px" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:21px;color:#666666;font-size:14px" >
        //                                                                        You received this email because you just signed
        //                                                                        up for a new account.If it looks weird,
        //                                                                        <strong><a class= "view" target = "_blank"
        //                                                                                href = "https://viewstripo.email"
        //                                                                                style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#111111;font-size:14px" > view
        //                                                                                it in your browser</a></strong>.</p>
        //                                                                </td>
        //                                                            </tr>
        //                                                            <tr style = "border-collapse:collapse" >
        //                                                                < td align= "left"
        //                                                                    style= "padding:0;Margin:0;padding-top:25px" >
        //                                                                    < p
        //                                                                        style= "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:21px;color:#666666;font-size:14px" >
        //                                                                        If these emails get annoying, please feel free
        //                                                                        to&nbsp;< strong >< a target = "_blank"
        //                                                                                class= "unsubscribe" href = ""
        //                                                                                style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#111111;font-size:14px" > unsubscribe </ a ></ strong >.
        //                                                                    </ p >
        //                                                                </ td >
        //                                                            </ tr >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td align = "left"
        //                                                                    style = "padding:0;Margin:0;padding-top:25px" >
        //                                                                    < p
        //                                                                        style = "Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:lato, 'helvetica neue', helvetica, arial, sans-serif;line-height:21px;color:#666666;font-size:14px" >
        //                                                                        Ceej - 1234 Main Street - Anywhere, MA - 56789
        //                                                                    </p>
        //                                                                </td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </table>
        //                    <table class= "es-content" cellspacing = "0" cellpadding = "0" align = "center"
        //                        style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%" >
        //                        < tr style = "border-collapse:collapse" >
        //                            < td align = "center" style = "padding:0;Margin:0" >
        //                                < table class= "es-content-body"
        //                                    style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"
        //                                    cellspacing = "0" cellpadding = "0" align = "center" >
        //                                    < tr style = "border-collapse:collapse" >
        //                                        < td align = "left"
        //                                            style = "Margin:0;padding-left:20px;padding-right:20px;padding-top:30px;padding-bottom:30px" >
        //                                            < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                < tr style = "border-collapse:collapse" >
        //                                                    < td valign = "top" align = "center"
        //                                                        style = "padding:0;Margin:0;width:560px" >
        //                                                        < table width = "100%" cellspacing = "0" cellpadding = "0"
        //                                                            role = "presentation"
        //                                                            style = "mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px" >
        //                                                            < tr style = "border-collapse:collapse" >
        //                                                                < td class= "es-infoblock made_with" align = "center"
        //                                                                    style = "padding:0;Margin:0;line-height:120%;font-size:0;color:#CCCCCC" >
        //                                                                    < a target = "_blank"
        //                                                                        href = "https://viewstripo.email/?utm_source=templates&utm_medium=email&utm_campaign=software2&utm_content=trigger_newsletter5"
        //                                                                        style = "-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#CCCCCC;font-size:12px" >< img
        //                                                                            src = "https://gupxey.stripocdn.email/content/guids/CABINET_9df86e5b6c53dd0319931e2447ed854b/images/64951510234941531.png"
        //                                                                            alt width = "125"
        //                                                                            style="display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic"></a>
        //                                                                </td>
        //                                                            </tr>
        //                                                        </table>
        //                                                    </td>
        //                                                </tr>
        //                                            </table>
        //                                        </td>
        //                                    </tr>
        //                                </table>
        //                            </td>
        //                        </tr>
        //                    </table>
        //                </td>
        //            </tr>
        //        </table>
        //    </div>
        //</body>

        //</html>
        //    """";

        return confirmEmailTemplate;


    }
}
