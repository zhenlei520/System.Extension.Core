// Copyright (c) zhenlei520 All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using System.Net.Mail;

namespace EInfrastructure.Core.HelpCommon
{
    /// <summary>
    /// 邮箱帮助类
    /// </summary>
    public class EMailCommon
    {
        #region 发送邮件

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="pToMail">目的邮件地址</param>
        /// <param name="pMailTitle">发送邮件的标题</param>
        /// <param name="pMailBoby">发送邮件的内容</param>
        /// <param name="pMailAddress">发送者的邮箱地址</param>
        /// <param name="pSmtpClient">网络上的代理服务器</param>
        /// <param name="pPwd">发送邮件的密码</param>
        /// <param name="returnstr">发送成功提示语，成功的话应该与pPwd一致</param>
        public static object SendEmail(string pToMail, string pMailTitle, string pMailBoby, string pMailAddress,
            string pSmtpClient, string pPwd, string returnstr)
        {
            //Attachment objMailAttachment;
            // 创建一个附件对象
            //objMailAttachment = new Attachment("f:\\世界杯赛程.rtf");//发送邮件的附件
            // 创建邮件消息
            var objMailMessage = new MailMessage {From = new MailAddress(pMailAddress)};
            //发送者的邮箱地址

            objMailMessage.To.Add(pToMail); //目的邮件地址

            objMailMessage.Subject = pMailTitle; //发送邮件的标题

            objMailMessage.Body = pMailBoby; //发送邮件的内容
            objMailMessage.IsBodyHtml = true;
            objMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            objMailMessage.Priority = MailPriority.Normal;
            //objMailMessage.Attachments.Add(objMailAttachment);//将附件附加到邮件消息对象中
            //SMTP地址
            SmtpClient smtpClient = new SmtpClient(pSmtpClient)
            {
                EnableSsl = false,
                Credentials = new System.Net.NetworkCredential(objMailMessage.From.Address, pPwd),
                DeliveryMethod = SmtpDeliveryMethod.Network
            }; //网络上的代理服务器
            //设置发件人身份的信息
            //smtpClient.Host = "smtp." + objMailMessage.From.Host;
            try
            {
                smtpClient.Send(objMailMessage);
                return new
                {
                    Status = true,
                    Msg = returnstr
                };
            }
            catch (SmtpException ex)
            {
                return new
                {
                    Status = false,
                    Msg = ex.Message
                };
            }
            catch (System.Exception ex)
            {
                return new
                {
                    Status = false,
                    Msg = ex.Message
                };
            }
        }

        #endregion
    }
}