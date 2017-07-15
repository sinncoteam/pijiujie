using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Security.Cryptography;
using System.Collections;

namespace JFB.Utils
{
    public class StringHelper
    {
        public static string Cut(string str, int len, Encoding encoding)
        {
            if (string.IsNullOrEmpty(str)) return str;
            if (len <= 0) return string.Empty;

            int byteCount = encoding.GetByteCount(str);
            if (byteCount > len)
            {
                bool isAscii = byteCount == str.Length; //判断是否为ASCII字符串
                if (isAscii)
                {
                    return str.Substring(0, len);
                }
                else
                {
                    int k = 0;
                    int index = 0;
                    char[] chars = str.ToCharArray();
                    foreach (char c in chars)
                    {
                        int charCount = encoding.GetByteCount(chars, index, 1);
                        int tmp = k + charCount;
                        if (tmp > len)
                        {
                            return str.Substring(0, index);
                        }
                        else
                        {
                            k = tmp;
                        }
                        index++;
                    }
                }
            }
            return str;
        }//end method

        /// <summary>
        /// 格式化回车换行代码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string HtmlFormat(string str)
        {
            if (string.IsNullOrEmpty(str)) return str;
            str = HtmlEncode(str);
            str = str.Replace("\r\n", "<br/>");
            str = str.Replace("\n", "<br/>");
            str = str.Replace(" ", "&nbsp;");
            return str;
        }

        /// <summary>
        /// 返回 HTML 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlEncode(string str)
        {
            return HttpUtility.HtmlEncode(str);
        }

        public static int GetUTF8Length(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }
            else
            {
                return System.Text.Encoding.UTF8.GetByteCount(s);
            }
        }

        /// <summary>
        /// 替换回车换行符为html换行符
        /// </summary>
        public static string StrFormat(string str)
        {
            string str2;

            if (str == null)
            {
                str2 = "";
            }
            else
            {
                str = str.Replace("\r\n", "<br />");
                str = str.Replace("\n", "<br />");
                str2 = str;
            }
            return str2;
        }

        /// <summary>
        /// 替换html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        /// <summary>
        /// 返回 HTML 字符串的解码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string HtmlDecode(string str)
        {

            return HttpUtility.HtmlDecode(str);
        }



        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string str)
        {
            return HttpUtility.UrlEncode(str);
        }

        /// <summary>
        /// 返回 URL 字符串的编码结果
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns>解码结果</returns>
        public static string UrlDecode(string str)
        {
            return HttpUtility.UrlDecode(str);
        }

        /// <summary>
        /// 返回相差的秒数
        /// </summary>
        /// <param name="Time"></param>
        /// <param name="Sec"></param>
        /// <returns></returns>
        public static int StrDateDiffSeconds(string Time, int Sec)
        {
            TimeSpan ts = DateTime.Now - DateTime.Parse(Time).AddSeconds(Sec);
            if (ts.TotalSeconds > int.MaxValue)
                return int.MaxValue;

            else if (ts.TotalSeconds < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalSeconds;
        }

        /// <summary>
        /// 返回相差的分钟数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int StrDateDiffMinutes(string time, int minutes)
        {
            if (StrIsNullOrEmpty(time))
                return 1;

            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddMinutes(minutes);
            if (ts.TotalMinutes > int.MaxValue)
                return int.MaxValue;
            else if (ts.TotalMinutes < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalMinutes;
        }

        /// <summary>
        /// 返回相差的小时数
        /// </summary>
        /// <param name="time"></param>
        /// <param name="hours"></param>
        /// <returns></returns>
        public static int StrDateDiffHours(string time, int hours)
        {
            if (StrIsNullOrEmpty(time))
                return 1;

            TimeSpan ts = DateTime.Now - DateTime.Parse(time).AddHours(hours);
            if (ts.TotalHours > int.MaxValue)
                return int.MaxValue;
            else if (ts.TotalHours < int.MinValue)
                return int.MinValue;

            return (int)ts.TotalHours;
        }

        /// <summary>
        /// 获取新时间差
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetTimeDiffNew(DateTime dt)
        {
            TimeSpan tp = DateTime.Now - dt;
            long IntervalTime = (long)tp.TotalMinutes;
            if (IntervalTime >= 7 * 24 * 60)//大于一周
                return dt.ToString("yy-MM-dd HH:mm");
            else if (IntervalTime >= 24 * 60)//大于一天
                return (IntervalTime / (24 * 60)).ToString() + "天前";
            else if (IntervalTime >= 60)//大于一小时
                return (IntervalTime / 60).ToString() + "小时前";
            else if (IntervalTime >= 1)
                return IntervalTime.ToString() + "分钟前";
            else
                return "1分钟前";
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                        startIndex = startIndex - length;
                }

                if (startIndex > str.Length)
                    return "";
            }
            else
            {
                if (length < 0)
                    return "";
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                        return "";
                }
            }

            if (str.Length - startIndex < length)
                length = str.Length - startIndex;

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }

        /// <summary>
        /// 获取指定长度字符串传
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStr(string contents, int length, bool append = false)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(contents) && contents.Length > length)
            {
                result = contents.Substring(0, length);
            }
            else
            {
                result = contents ?? "";
            }
            if (append)
            {
                result += "...";
            }

            return result;
        }

        /// <summary>
        /// 字符串如果操过指定长度则将超出的部分用指定字符串代替
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_Length, string p_TailString)
        {
            return GetSubString(p_SrcString, 0, p_Length, p_TailString);
        }

        /// <summary>
        /// 取指定长度的字符串
        /// </summary>
        /// <param name="p_SrcString">要检查的字符串</param>
        /// <param name="p_StartIndex">起始位置</param>
        /// <param name="p_Length">指定长度</param>
        /// <param name="p_TailString">用于替换的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string GetSubString(string p_SrcString, int p_StartIndex, int p_Length, string p_TailString)
        {
            string myResult = p_SrcString;

            Byte[] bComments = Encoding.UTF8.GetBytes(p_SrcString);
            foreach (char c in Encoding.UTF8.GetChars(bComments))
            {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
                if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
                {
                    //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                    //当截取的起始位置超出字段串长度时
                    if (p_StartIndex >= p_SrcString.Length)
                        return "";
                    else
                        return p_SrcString.Substring(p_StartIndex,
                                                       ((p_Length + p_StartIndex) > p_SrcString.Length) ? (p_SrcString.Length - p_StartIndex) : p_Length);
                }
            }

            if (p_Length >= 0)
            {
                byte[] bsSrcString = Encoding.Default.GetBytes(p_SrcString);

                //当字符串长度大于起始位置
                if (bsSrcString.Length > p_StartIndex)
                {
                    int p_EndIndex = bsSrcString.Length;

                    //当要截取的长度在字符串的有效长度范围内
                    if (bsSrcString.Length > (p_StartIndex + p_Length))
                    {
                        p_EndIndex = p_Length + p_StartIndex;
                    }
                    else
                    {   //当不在有效范围内时,只取到字符串的结尾

                        p_Length = bsSrcString.Length - p_StartIndex;
                        p_TailString = "";
                    }

                    int nRealLength = p_Length;
                    int[] anResultFlag = new int[p_Length];
                    byte[] bsResult = null;

                    int nFlag = 0;
                    for (int i = p_StartIndex; i < p_EndIndex; i++)
                    {
                        if (bsSrcString[i] > 127)
                        {
                            nFlag++;
                            if (nFlag == 3)
                                nFlag = 1;
                        }
                        else
                            nFlag = 0;

                        anResultFlag[i] = nFlag;
                    }

                    if ((bsSrcString[p_EndIndex - 1] > 127) && (anResultFlag[p_Length - 1] == 1))
                        nRealLength = p_Length + 1;

                    bsResult = new byte[nRealLength];

                    Array.Copy(bsSrcString, p_StartIndex, bsResult, 0, nRealLength);

                    myResult = Encoding.Default.GetString(bsResult);
                    myResult = myResult + p_TailString;
                }
            }

            return myResult;
        }

        /// <summary>
        /// 判断一个字符串是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        /// 
        public static bool IsEmpty(string s)
        {
            return s == null || s.Length == 0;
        }

        /// <summary>
        /// 判断一个字符串被剪切后是否为空
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsTrimEmpty(string s)
        {
            return s == null || s.Length == 0 || s.Trim().Length == 0;
        }

        /// <summary>
        /// 效验字符串是否以字母开头，符合语法规范
        /// 
        /// PS:忽略大小写

        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsUserName(string s)
        {
            return Regex.IsMatch(s, @"^[a-z]\w{3,19}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 效验字符是正确的电子邮件格式
        /// 
        /// PS:忽略大小写

        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEMail(string s)
        {
            return Regex.IsMatch(s, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否是整数
        /// 
        /// PS:忽略大小写
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsDigit(string s)
        {
            return Regex.IsMatch(s, @"^\d+$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 判断字符串是否是正确的电话号码书写格式

        /// 
        /// PS:忽略大小写

        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsPhone(string s)
        {
            return Regex.IsMatch(s, @"^((\(\d{2,3}\))|(\d{3}\-))?(\(0\d{2,3}\)|0\d{2,3}-)?[1-9]\d{6,7}(\-\d{1,4})?$", RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 判断字符串是否为正确的手机号码格式

        /// 
        /// PS:忽略大小写

        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsMoble(string s)
        {
            return Regex.IsMatch(s, @"^(13[0-9]|15[0|1|3|6|7|8|9]|18[8|9|6])\d{8}$", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 返回包含中文字符的字符串长度
        /// C# 的string.Length中中文字只做1位统计,所以要将其转换为2位

        /// </summary>
        /// <param name="strSource">要统计长度的字符串变量</param>
        /// <returns>字符串长度</returns>
        public static int GetLength(string strSource)
        {
            Regex regex = new Regex("[\u4e00-\u9fa5]+", RegexOptions.Compiled);
            int nLength = strSource.Length;

            for (int i = 0; i < strSource.Length; i++)
            {
                if (regex.IsMatch(strSource.Substring(i, 1)))
                {
                    nLength++;
                }
            }

            return nLength;
        }

        /// <summary>
        /// 是否为ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static bool IsIP(string ip)
        {
            return Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJSON(System.Data.DataTable dt)
        {
            return DataTableToJson(dt, true);
        }

        /// <summary>
        /// 将数据表转换成JSON类型串
        /// </summary>
        /// <param name="dt">要转换的数据表</param>
        /// <param name="dispose">数据表转换结束后是否dispose掉</param>
        /// <returns></returns>
        public static StringBuilder DataTableToJson(System.Data.DataTable dt, bool dt_dispose)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //数据表字段名和类型数组
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","号

            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //对 \ , ' 符号进行转换 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","号

            if (dt_dispose)
                dt.Dispose();

            return stringBuilder.Append("\r\n];");
        }

        /// <summary>
        /// 返回标准日期格式string
        /// </summary>
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回指定日期格式
        /// </summary>
        public static string GetDate(string datetimestr, string replacestr)
        {
            if (datetimestr == null)
                return replacestr;

            if (datetimestr.Equals(""))
                return replacestr;

            try
            {
                datetimestr = Convert.ToDateTime(datetimestr).ToString("yyyy-MM-dd").Replace("1900-01-01", replacestr);
            }
            catch
            {
                return replacestr;
            }
            return datetimestr;
        }


        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回相对于当前时间的相对天数
        /// </summary>
        public static string GetDateTime(int relativeday)
        {
            return DateTime.Now.AddDays(relativeday).ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间格式string
        /// </summary>
        public static string GetDateTimeF()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffffff");
        }

        /// <summary>
        /// 返回标准时间 
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime, string formatStr)
        {
            if (fDateTime == "0000-0-0 0:00:00")
                return fDateTime;
            DateTime time = new DateTime(1900, 1, 1, 0, 0, 0, 0);
            if (DateTime.TryParse(fDateTime, out time))
                return time.ToString(formatStr);
            else
                return "N/A";
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd HH:mm:ss
        /// </sumary>
        public static string GetStandardDateTime(string fDateTime)
        {
            return GetStandardDateTime(fDateTime, "yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 返回标准时间 yyyy-MM-dd
        /// </sumary>
        public static string GetStandardDate(string fDate)
        {
            return GetStandardDateTime(fDate, "yyyy-MM-dd");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsTime(string timeval)
        {
            return Regex.IsMatch(timeval, @"^((([0-1]?[0-9])|(2[0-3])):([0-5]?[0-9])(:[0-5]?[0-9])?)$");
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.UTF8.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');

            return ret;
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public static void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return HttpContext.Current.Request.Cookies[strName].Value.ToString();

            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return HttpContext.Current.Request.Cookies[strName][key].ToString();

            return "";
        }

        /// <summary>
        /// 移除Html标记
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveHtml(string content)
        {
            return Regex.Replace(content, @"<[^>]*>", string.Empty, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 过滤HTML中的不安全标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string RemoveUnsafeHtml(string content)
        {
            content = Regex.Replace(content, @"(\<|\s+)o([a-z]+\s?=)", "$1$2", RegexOptions.IgnoreCase);
            content = Regex.Replace(content, @"(script|frame|form|meta|behavior|style)([\s|:|>])+", "$1.$2", RegexOptions.IgnoreCase);
            return content;
        }

        /// <summary>
        /// 获得当前绝对路径
        /// </summary>
        /// <param name="strPath">指定的路径</param>
        /// <returns>绝对路径</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// 内容分页
        /// </summary>
        /// <param name="source">源内容</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageCount">总页数</param>
        /// <param name="splitChar">分页分隔符</param>
        /// <param name="remain">余下全文</param>
        /// <returns>分页结果</returns>
        public static string GetByPage(string source, int pageIndex, out int pageCount, bool remain = false, string splitChar = "[[PAGE]]")
        {
            if (string.IsNullOrEmpty(source))
            {
                pageCount = 1;
                return "";
            }

            string[] resultArray = source.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);
            pageCount = resultArray.Count();
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }
            else if (pageIndex > pageCount)
            {
                pageIndex = pageCount;
            }

            string reValue = "";
            if (remain)
            {
                for (int i = pageIndex; i < resultArray.Count(); i++)
                {
                    reValue += resultArray[i];
                }
            }
            else
            {
                reValue = resultArray[pageIndex - 1];
            }

            return reValue;
        }

        /// <summary>
        /// 根据日期算出星座
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //1月20~2月18   水瓶座
        //2月19~3月20   双鱼座
        //3月21~4月19   白羊座
        //4月20~5月20   金牛座
        //5月21~6月21   双子座
        //6月22~7月22   巨蟹座
        //7月23~8月22   狮子座
        //8月23~9月22   处女座
        //9月23~10月23  天枰座
        //10月24~11月22 天蝎座
        //11月23~12月21 射手座
        //12月22~1月19  摩羯座
        public static string GetHoroscope(DateTime dt)
        {
            //如果时间为Null或者为0001-01-01时，返回空字符串
            if (DateIsNullOrDefault(dt))
            {
                return string.Empty;
            }
            string result = string.Empty;
            int month = dt.Month;
            int day = dt.Day;
            if ((month == 1 && day >= 20) || (month == 2 && day <= 18))
            {
                result = "水瓶座";
            }
            else if ((month == 2 && day >= 19) || (month == 3 && day <= 20))
            {
                result = "双鱼座";
            }
            else if ((month == 3 && day >= 21) || (month == 4 && day <= 19))
            {
                result = "白羊座";
            }
            else if ((month == 4 && day >= 20) || (month == 5 && day <= 20))
            {
                result = "金牛座";
            }
            else if ((month == 5 && day >= 21) || (month == 6 && day <= 21))
            {
                result = "双子座";
            }
            else if ((month == 6 && day >= 22) || (month == 7 && day <= 22))
            {
                result = "巨蟹座";
            }
            else if ((month == 7 && day >= 23) || (month == 8 && day <= 22))
            {
                result = "狮子座";
            }
            else if ((month == 8 && day >= 23) || (month == 9 && day <= 22))
            {
                result = "处女座";
            }
            else if ((month == 9 && day >= 23) || (month == 10 && day <= 23))
            {
                result = "天枰座";
            }
            else if ((month == 10 && day >= 24) || (month == 11 && day <= 22))
            {
                result = "天蝎座";
            }
            else if ((month == 11 && day >= 23) || (month == 12 && day <= 21))
            {
                result = "射手座";
            }
            else if ((month == 12 && day >= 22) || (month == 1 && day <= 19))
            {
                result = "摩羯座";
            }

            return result;
        }

        public static bool DateIsNullOrDefault(DateTime dt)
        {
            if (null == dt || dt == new DateTime(1, 1, 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 转半角的函数
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        ///<remarks>
        ///全角空格为12288，半角空格为32
        ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
        ///</remarks>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 是否为中文或者字母数字下划线的字符串
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static bool IsChineseStr(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return true;
            }
            string reg = @"^([\u4e00-\u9fa5]|\w)+$";
            if (Regex.IsMatch(content, reg, RegexOptions.None))
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// 替换HTML字符
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceHtml(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            StringBuilder buf = new StringBuilder();
            char ch = ' ';
            for (int i = 0; i < content.Length; i++)
            {
                ch = content[i];
                if (ch == '<')
                {
                    buf.Append("&lt;");
                }
                else if (ch == '>')
                {
                    buf.Append("&gt;");
                }
                else if (ch == '"')
                {
                    buf.Append("&quot;");
                }
                else if (ch == '&')
                {
                    buf.Append("&amp;");
                }
                else if (ch == '\'')
                {
                    buf.Append(' ');
                }
                else
                {
                    buf.Append(ch);
                }
            }
            return buf.ToString();
        }


        /// <summary>
        /// 展示标题过滤
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="publishFrom">发布来源 1.前台 2.后台</param>
        /// <returns></returns>
        public static string TitleFilter(string source, int publishFrom)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            if (publishFrom == 1)
            {
                //SensetiveHelper sh = SensetiveHelper.Instance;
                //source = sh.ReplaceContentSensetive(source);
                source = ReplacePhoneNumber(source);
                source = ReplaceWebSite(source);
            }

            return source;
        }
      
        /// <summary>
        /// 替换文中的电话号码 显示后4位
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static string ReplacePhoneNumber(string source)
        {
            //匹配电话号码
            MatchCollection collection = Regex.Matches(source, @"\d{11}");  // @"1[3|4|5|8][0-9]\d{9}"
            ArrayList listPlacePhone = new ArrayList();
            for (int i = 0; i < collection.Count; i++)
            {
                string currentphone = collection[i].ToString();
                string placephone = currentphone.Substring(0, currentphone.Length - 4);
                placephone = placephone + "****";
                source = source.Replace(currentphone.Trim(), placephone.Trim());
            }
            return source;
        }

        /// <summary>
        /// 替换网址
        /// </summary>
        /// <param name="source">数据源</param>
        /// <returns></returns>
        public static string ReplaceWebSite(string source)
        {
            MatchCollection collection = Regex.Matches(source, @"(http:\/\/)?[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-_~`@[\]\':+!]*([^<>\""])*?[\s|(\u0391-\uFFE5)]?", RegexOptions.IgnoreCase);
            ArrayList listPlacePhone = new ArrayList();
            for (int i = 0; i < collection.Count; i++)
            {
                string currentwebsite = collection[i].ToString();
                source = source.Replace(currentwebsite, "***");
            }

            return source;
        }

        public static string SetWapUrl(string source, string preview)
        {
            source = SetImageUrl(source, preview);
            return SetLinkUrl(source, preview);
        }

        public static string SetImageUrl(string source, string preview)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            Regex reg = new Regex(@"(?<=<img.*src=(['""]?))(?!http://)(?=[^'""\s>]+\1)");
            return reg.Replace(source, preview);
        }

        public static string SetLinkUrl(string source, string preview)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return string.Empty;
            }

            Regex reg = new Regex(@"(?<=<a.*href=(['""]?))(?!http://)(?=[^'""\s>]+\1)");
            return reg.Replace(source, preview);
        }

        /*
         * 理论部分：
         * 15位身份证号码=6位地区代码+6位生日+3位编号
         * 18位身份证号码=6位地区代码+8位生日+3位编号+1位检验码
         * 
         * 各省市地区国家代码前两位代码是：     
         * 北京 11 吉林 22 福建 35 广东 44 云南 53 天津 12 黑龙江 23 江西 36 广西 45 西藏 54 河北 13 上海 31 山东 37 海南 46 陕西 61 山西 14 江苏 32 河南 41 重庆 50 
        甘肃 62 内蒙古 15 浙江 33 湖北 42 四川 51 青海 63 辽宁 21 安徽 34 湖南 43 贵州 52 宁夏 64 新 疆 65 台湾 71 香港 81 澳门 82 国外 91   
        *18位身份证标准在国家质量技术监督局于1999年7月1日实施的GB11643-1999《公民身份号码》中做了明确规定。
        *GB11643-1999《公民身份号码》为GB11643-1989《社会保障号码》的修订版，其中指出将原标准名称“社会保障号码”更名为“公民身份号码”，另外GB11643-1999《公民身份号码》从实施之日起代替GB11643-1989。
        *公民身份号码是特征组合码，由十七位数字本体码和一位校验码组成。排列顺序从左至右依次为：六位数字地址码，八位数字出生日期码，三位数字顺序码和一位校验码。其含义如下：
        *1. 地址码：表示编码对象常住户口所在县(市、旗、区)的行政区划代码，按GB/T2260的规定执行。
        *2. 出生日期码：表示编码对象出生的年、月、日，按GB/T7408的规定执行，年、月、日分别用4位、2位、2位数字表示，之间不用分隔符。
        *3. 顺序码：表示在同一地址码所标识的区域范围内，对同年、同月、同日出生的人编定的顺序号，顺序码的奇数分配给男性，偶数分配给女性。
        *校验的计算方式：
        *1. 对前17位数字本体码加权求和
        *公式为：S = Sum(Ai * Wi), i = 0, ... , 16
        *其中Ai表示第i位置上的身份证号码数字值，Wi表示第i位置上的加权因子，其各位对应的值依次为： 
        *7 9 10 5 8 4 2 1 6 3 7 9 10 5 8 4 2
        *2. 以11对计算结果取模
        *Y = mod(S, 11)
        *3. 根据模的值得到对应的校验码对应关系为：
        *Y值： 0 1 2 3 4 5 6 7 8 9 10
        *校验码： 1 0 X 9 8 7 6 5 4 3 2
        */
        /// <summary>
        /// 身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        public static bool CheckIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = CheckIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = CheckIDCard15(Id);
                return check;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 18位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard18(string Id)
        {
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        /// <summary>
        /// 15位身份证验证
        /// </summary>
        /// <param name="Id">身份证号</param>
        /// <returns></returns>
        private static bool CheckIDCard15(string Id)
        {
            long n = 0;
            if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 格式化手机号码
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>139****9999</returns>
        public static string FormatPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return phone;
            phone = phone.Trim();
            long _phone = 0;
            if (long.TryParse(phone, out _phone) && phone.Length == 11)
            {
                Regex r = new Regex(@"\d{3}(\d{4})\d+");
                string m = r.Match(phone).Result("$1");
                return phone.Replace(m, "****");
            }
            return phone;
        }

        /// <summary>
        /// 将long型转换成datetime
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DateTime LongToDate(long dt)
        {
            try
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(dt + "0000");
                TimeSpan toNow = new TimeSpan(lTime);
                DateTime dtResult = dtStart.Add(toNow);
                return dtResult;
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 从字符串中获取数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string getIntStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            string result = "";
            var rator = str.GetEnumerator();
            while (rator.MoveNext())
            {
                string c = rator.Current.ToString();
                int i;
                if (int.TryParse(c, out i))
                {
                    result += c;
                }
            }
            return result;
        }

    }//end class
}
