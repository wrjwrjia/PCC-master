using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

/// <summary>
///ExpressionCalculate 的摘要说明
/// </summary>
public class ExpressionCalculate
{
	    /// <summary>
        /// 私有成员变量，字符串类型。主要用于存储表示原始中缀表达式的字符串。
        /// </summary>
	    public string m_string;

        /// <summary>
        ///根据输入的ProductNumber，获取对应所属Family的计算公式
        /// 并将公式转化为数字和操作符表示的表达式:ex:(1.00+2+2/4)的形式
        /// </summary>
        /// <param name="ProductNumber"></param>
        /// <param name="ProcessName"></param>
        /// <param name="Date"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetExpression(string ProductNumber,string ProcessName,string Date,string Type,string newtype)
        {
            //获取设定的公式
            string str = string.Empty;
            DataTable exp_dt = SQLHelper.GetDataTable("select expression from tbl_expression where Type = '" + Type + "' and newtype = '" + newtype + "' and FamilyName =(select ProductFamily from tbl_productFamily where ProductFamilyNumber = (select ProductFamilyNumber from tbl_product where ProductNumber = '" + ProductNumber + "'))");
            if (exp_dt != null && exp_dt.Rows.Count > 0)
            {
                str = exp_dt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

            if (str=="max(station)")
            {
                return "max(station)";
            }
            if (str == "sum(station)")
            {
                return "sum(station)";
            }

            //str = Regex.Replace(str, @"\s", "");
            string strdemo;
            char[] separator = { '+', '-', '*', '/', '(', ')' };
            String[] splitStrings = new String[100];
            String[] splitStrings1 = new String[100];
            splitStrings1 = str.Split(separator);
            List<string> list = new List<string>();
            foreach (string s in splitStrings1)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }

            splitStrings = list.ToArray();

            int m = 0;
            while (m < splitStrings.Length)
            {
                splitStrings[m] = splitStrings[m].Trim();
                if (splitStrings[m].Length > 0)
                {
                    if (IsNumberic(splitStrings[m]))
                    {
                        m++;
                        continue;
                    }
                    strdemo = SQLHelper.ReturnDouble("select ParameterValue from tbl_productivityparametervalue where ProductNumber = '" + ProductNumber + "' and  ProcessName = '" + ProcessName + "' and Date = '" + Date+ "' and parametername = '" + splitStrings[m].Trim() + "'").ToString();
                    str = str.Replace(splitStrings[m], strdemo);
                }
                m++;
            }

            return str;
        }


        /// <summary>
        ///根据输入的ProductNumber，获取对应所属Family的计算公式
        /// 并将公式转化为数字和操作符表示的表达式:ex:(1.00+2+2/4)的形式
        /// </summary>
        /// <param name="ProductNumber"></param>
        /// <param name="ProcessName"></param>
        /// <param name="Date"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetExpressionProductivity(string ProductNumber, string ProcessName, string Date, string HourPerShift, string AverageTime, string Type, string newtype)
        {
            //获取设定的公式
            string str = string.Empty;
            DataTable exp_dt = SQLHelper.GetDataTable("select expression from tbl_expression where Type = '" + Type + "' and newtype='" + newtype + "' and FamilyName =(select ProductFamily from tbl_productFamily where ProductFamilyNumber = (select ProductFamilyNumber from tbl_product where ProductNumber = '" + ProductNumber + "'))");
            if (exp_dt != null && exp_dt.Rows.Count > 0)
            {
                str = exp_dt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

            str = str.Replace("Hour per shift",HourPerShift);
            str = str.Replace("Average Time", AverageTime);

            string strdemo;
            char[] separator = { '+', '-', '*', '/', '(', ')' };
            String[] splitStrings = new String[100];
            String[] splitStrings1 = new String[100];
            splitStrings1 = str.Split(separator);
            List<string> list = new List<string>();
            foreach (string s in splitStrings1)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }

            splitStrings = list.ToArray();

            int m = 0;
            while (m < splitStrings.Length)
            {
                splitStrings[m] = splitStrings[m].Trim();
                if (splitStrings[m].Length > 0)
                {
                    if (PageValidate.isNumber(splitStrings[m]))
                    {
                        m++;
                        continue;
                    }
                    strdemo = SQLHelper.ReturnDouble("select ParameterValue from tbl_productivityparametervalue where ProductNumber = '" + ProductNumber + "' and  ProcessName = '" + ProcessName + "' and Date = '" + Date + "' and parametername = '" + splitStrings[m].Trim() + "'").ToString();
                    str = str.Replace(splitStrings[m], strdemo);
                }
                m++;
            }

            return str;
        }

        /// <summary>
        ///根据输入的ProductNumber，获取对应所属Family的计算公式
        /// 并将公式转化为数字和操作符表示的表达式:ex:(1.00+2+2/4)的形式
        /// </summary>
        /// <param name="ProductNumber"></param>
        /// <param name="ProcessName"></param>
        /// <param name="Date"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetExpressionForNode(string ProductNumber, string ProcessName, string Date, string Type, string newtype,string newnewtype)
        {
            //获取设定的公式
            string str = string.Empty;
            DataTable exp_dt = SQLHelper.GetDataTable("select expression from tbl_expression where Type = '" + Type + "' and newtype = '" + newtype + "'  and newnewtype = '" + newnewtype + "' and FamilyName =(select ProductFamily from tbl_productFamily where ProductFamilyNumber = (select ProductFamilyNumber from tbl_product where ProductNumber = '" + ProductNumber + "'))");
            if (exp_dt != null && exp_dt.Rows.Count > 0)
            {
                str = exp_dt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

            if (str == "max(station)")
            {
                return "max(station)";
            }
            if (str == "sum(station)")
            {
                return "sum(station)";
            }

            //str = Regex.Replace(str, @"\s", "");
            string strdemo;
            char[] separator = { '+', '-', '*', '/', '(', ')' };
            String[] splitStrings = new String[100];
            String[] splitStrings1 = new String[100];
            splitStrings1 = str.Split(separator);
            List<string> list = new List<string>();
            foreach (string s in splitStrings1)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }

            splitStrings = list.ToArray();

            int m = 0;
            while (m < splitStrings.Length)
            {
                splitStrings[m] = splitStrings[m].Trim();
                if (splitStrings[m].Length > 0)
                {
                    if (IsNumberic(splitStrings[m]))
                    {
                        m++;
                        continue;
                    }
                    strdemo = SQLHelper.ReturnDouble("select ParameterValue from tbl_productivityparametervalue where ProductNumber = '" + ProductNumber + "' and  ProcessName = '" + ProcessName + "' and Date = '" + Date + "' and parametername = '" + splitStrings[m].Trim() + "'").ToString();
                    str = str.Replace(splitStrings[m], strdemo);
                }
                m++;
            }

            return str;
        }


        /// <summary>
        ///根据输入的ProductNumber，获取对应所属Family的计算公式
        /// 并将公式转化为数字和操作符表示的表达式:ex:(1.00+2+2/4)的形式
        /// </summary>
        /// <param name="ProductNumber"></param>
        /// <param name="ProcessName"></param>
        /// <param name="Date"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public string GetExpressionProductivityForNode(string ProductNumber, string ProcessName, string Date, string HourPerShift, string AverageTime, string Type, string newtype, string newnewtype)
        {
            //获取设定的公式
            string str = string.Empty;
            DataTable exp_dt = SQLHelper.GetDataTable("select expression from tbl_expression where Type = '" + Type + "' and newtype='" + newtype + "' and newnewtype = '" + newnewtype + "' and FamilyName =(select ProductFamily from tbl_productFamily where ProductFamilyNumber = (select ProductFamilyNumber from tbl_product where ProductNumber = '" + ProductNumber + "'))");
            if (exp_dt != null && exp_dt.Rows.Count > 0)
            {
                str = exp_dt.Rows[0][0].ToString();
            }
            else
            {
                return string.Empty;
            }

            str = str.Replace("Hour per shift", HourPerShift);
            str = str.Replace("Average Time", AverageTime);

            string strdemo;
            char[] separator = { '+', '-', '*', '/', '(', ')' };
            String[] splitStrings = new String[100];
            String[] splitStrings1 = new String[100];
            splitStrings1 = str.Split(separator);
            List<string> list = new List<string>();
            foreach (string s in splitStrings1)
            {
                if (!list.Contains(s))
                {
                    list.Add(s);
                }
            }

            splitStrings = list.ToArray();

            int m = 0;
            while (m < splitStrings.Length)
            {
                splitStrings[m] = splitStrings[m].Trim();
                if (splitStrings[m].Length > 0)
                {
                    if (PageValidate.isNumber(splitStrings[m]))
                    {
                        m++;
                        continue;
                    }
                    strdemo = SQLHelper.ReturnDouble("select ParameterValue from tbl_productivityparametervalue where ProductNumber = '" + ProductNumber + "' and  ProcessName = '" + ProcessName + "' and Date = '" + Date + "' and parametername = '" + splitStrings[m].Trim() + "'").ToString();
                    str = str.Replace(splitStrings[m], strdemo);
                }
                m++;
            }

            return str;
        }


        
        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="oText"></param>
        /// <returns></returns>
        private bool IsNumberic(string oText)
        {
            try
            {
                int var1 = Convert.ToInt32(oText);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 构造函数，给m_string赋值
        /// </summary>
        public ExpressionCalculate()
        {
            m_string = "";
        }

        /// <summary>
        /// 带参数的构造函数,给m_string赋值
        /// </summary>
        /// <param name="m_string"></param>
        public ExpressionCalculate(string m_string)
        {
            	this.m_string = m_string;
        }
        
        /// <summary>
	    /// 返回 double 类型的值。其功能是计算转换后
        /// 的后缀表达式的值，即原始中缀表达式的值。
	    /// </summary>
	    /// <returns></returns>
        public double Calculate()
        {
            Stack stack_A = ChangeToSuffix();// 取得后缀表达式
            if (stack_A.Count==0)
                return 0;
            Stack stack = new Stack();
            string str;
            char ch;
            double dbl; 
            while (stack_A.Count!=0)
            {
                str = stack_A.Peek().ToString();
                stack_A.Pop();
                ch = str[0];
                switch (ch)
                {
                    case '+':
                        dbl = Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        dbl += Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        stack.Push(dbl);
                        break;
                    case '-':
                        dbl = Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        dbl = Convert.ToDouble(stack.Peek().ToString()) -dbl;
                        stack.Pop();
                        stack.Push(dbl);
                        break;
                    case '*':
                        dbl = Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        dbl *= Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        stack.Push(dbl);
                        break;
                    case '/':
                        dbl = Convert.ToDouble(stack.Peek().ToString());
                        stack.Pop();
                        if (dbl != 0) // 除数不为 0！！！
                        {
                            dbl = Convert.ToDouble(stack.Peek().ToString())/ dbl;
                            stack.Pop();
                            stack.Push(dbl);
                        }
                        else
                        {
                            //除数为时结果为0
                            //JScript.Alert("divided by zero");
                            return 0;
                        }
                        break;
                    default:
                        // 将字符串所代表的操作数转换成双精度浮点数
                        // 并压入栈
                        stack.Push(Convert.ToDouble(str));
                        break;
                }
            }
            return Convert.ToDouble(stack.Peek().ToString());
        }


        /// <summary>
        /// 私有成员函数，返回值是一个 string 类型的
        /// 队列。其功能是将原始的中缀表达式中的操作数、操作符以及括号按顺序以
        /// 字符串的形式分解出来，然后保存在一个队列中
        /// </summary>
        /// <returns></returns>
        public Queue DivideExpressionToItem()
        {
            Queue que = new Queue();
            if (!IsWellForm())// 括号是否匹配
            {
                Console.WriteLine("The original expression is not well-form. Please check it and try again!");
                return que;
            }
            string str = "";
            char ch;
            int size = Size();
            bool bNumber = false;
            for (int i = 0; i < size; i++)
            {
                ch = m_string[i];
                switch (ch)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                    case '.':
                        bNumber = true;
                        break;
                    case '(':
                    case ')':
                    case '+':
                    case '-':
                    case '*':
                    case '/':
                        bNumber = false;
                        break;
                    default: continue;
                }
                if (bNumber)
                {
                    str += ch;
                    if (i == size - 1)
                        que.Enqueue(str);
                }
                else
                {
                    if (str.Length != 0)
                        que.Enqueue(str);
                    str = ch.ToString();
                    que.Enqueue(str); str = "";
                }
            }
            return que;
        }
	  
        /// <summary>
        /// 返回值是一个 string 类型的栈。
        /// 其功能是将队列中表示原始表达式各项的字符串调整顺序，转换成后缀表达式的
        /// 顺序，并处理掉括号，然后保存在一个栈中
        /// </summary>
        /// <returns></returns>
        public Stack ChangeToSuffix()
        {
            Queue que = new Queue();
            Stack stack_A = new Stack();
            Stack stack_B = new Stack();
            que = DivideExpressionToItem(); // 取得中缀表达式队列
            if (que.Count==0)
                return stack_B;
            string str;
            while (que.Count!=0)
            {
                str = que.Peek().ToString();
                que.Dequeue();
                if (str == "(")
                {
                    stack_B.Push(str);
                }
                else if (str == ")")
                {
                    while (stack_B.Count!=0 && stack_B.Peek().ToString() != "(")
                    {
                        stack_A.Push(stack_B.Peek());
                        stack_B.Pop();
                    }
                    if (stack_B.Count!=0)
                        stack_B.Pop();
                }
                else if (str == "+" || str == "-")
                {
                    if (stack_B.Count == 0 || stack_B.Peek().ToString() == "(")
                    {
                        stack_B.Push(str);
                    }
                    else
                    {
                        while (stack_B.Count != 0 && stack_B.Peek().ToString() != "(")
                        {
                            stack_A.Push(stack_B.Peek());
                            stack_B.Pop();
                        }
                        stack_B.Push(str);
                    }
                }
                else if (str == "*" || str == "/")
                {
                    if (stack_B.Count == 0 || stack_B.Peek().ToString() == "+" || stack_B.Peek().ToString() == "-" || stack_B.Peek().ToString() == "(")
                    {
                        stack_B.Push(str);
                    }
                    else
                    {
                        stack_A.Push(stack_B.Peek());
                        stack_B.Pop();
                        stack_B.Push(str);
                    }
                }
                else
                    stack_A.Push(str);
            }
            while (stack_B.Count!=0) // 如果 stack_B 中还有操作符则将其弹出并推入 stack_A
            {
                if (stack_B.Peek().ToString() != "(")
                    stack_A.Push(stack_B.Peek());
                stack_B.Pop();
            }
            while (stack_A.Count!=0)
            {
                stack_B.Push(stack_A.Peek());
                Console.WriteLine(stack_A.Peek() + " ");
                stack_A.Pop();
            }
            return stack_B;
        }
	   
        /// <summary>
        /// 返回 bool 值。其功能是判断原始表达式中的
        /// 括号是否匹配，如果匹配返回 true，否则返回 false。
        /// </summary>
        /// <returns></returns>
        public bool IsWellForm()
        {
            Stack stack = new Stack();
	        int size = Size();
	        char ch;
	        for(int i = 0; i < size; i++)
	        {
		        ch = m_string[i];
		        switch(ch)
		        {
		            case '(':
			            stack.Push(ch);
                        break;
		            case ')':
			            if(stack.Count==0)
				            return false;
			            else
				            stack.Pop();
			            break;
		            default:
                        break;
		        }
	        }

            if (stack.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        /// <summary>
        /// 返回原始表达式所包含的字节数
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return m_string.Length;
        }


}
