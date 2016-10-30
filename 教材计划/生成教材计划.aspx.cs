using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class 教材计划_生成教材计划 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string year = DateTime.Now.Year.ToString();
            string year2 = (Convert.ToInt32(year) - 1).ToString() + "-" + year;
            DropDownList1.Items.Add(new ListItem(year2));
            string year1 = year + "-" + (Convert.ToInt32(year) + 1).ToString();
            DropDownList1.Items.Add(new ListItem(year1));
            DropDownList1.SelectedIndex = 0;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        //判断
        if (DropDownList1.SelectedValue == "" || DropDownList2.SelectedValue == "")
        {
            WebMessageBox.Show("请选择学年和学期！");
            return;
        }

        string _year = DropDownList1.SelectedValue;
        string _semester = DropDownList2.SelectedValue;

        //第一步：整合Excel表（教学计划表中state=0的）到Ebook中：teacher--course（^隔开的分开）
        string date_year = _year, semester = _semester, course_id, course_name, course_college, teacher_id, teacher_name;
        SqlDataReader read = SQLHelper.ExecuteReader("select * from Excel where state=0");
        for (; read.Read(); )
        {
            date_year = read["date_year"].ToString();
            semester = read["semester"].ToString();
            course_id = read["course_id"].ToString();
            course_name = read["course_name"].ToString();
            course_college = read["course_college"].ToString();
            teacher_id = read["teacher_id"].ToString();
            teacher_name = read["teacher_name"].ToString();
            if (teacher_id.IndexOf('^') >= 0)
            {
                string[] id_array = teacher_id.Split('^');
                string[] name_array = teacher_name.Split('^');
                for (int i = 0; i < id_array.Length; i++)
                    SQLHelper.ExecuteNonQuery("insert into Ebook_message (date_year,semester,course_id,course_name,course_college,teacher_id,teacher_name,get_id,state) values('" + _year + "','" + _semester + "','" + course_id + "','" + course_name + "','" + course_college + "','" + id_array[i] + "','" + name_array[i] + "','9',0)");
            }
            else
                SQLHelper.ExecuteNonQuery("insert into Ebook_message (date_year,semester,course_id,course_name,course_college,teacher_id,teacher_name,get_id,state) values('" + _year + "','" + _semester + "','" + course_id + "','" + course_name + "','" + course_college + "','" + teacher_id + "','" + teacher_name + "','9',0)");
        }
        //把之前从Excel中新整理过的信息state改为1
        string ChangeEx = "update Excel set state=1 where state=0";
        SQLHelper.ExecuteNonQuery(ChangeEx);



        //第二步：整合Rbook_message表（教材选用登记表）到Ebook_message中：teacher--course--book
        int min = SQLHelper.ReturnInteger("select * from Ebook_message where state=0 order by id asc");
        int max = SQLHelper.ReturnInteger("select * from Ebook_message where state=0 order by id desc");
        for (int a = max; a >= min; a--)
        {
            SqlDataReader read1 = SQLHelper.ExecuteReader("select * from Ebook_message where id=" + a.ToString());
            for (; read1.Read(); )
            {
                string course_id1 = read1["course_id"].ToString();
                string course_name1 = read1["course_name"].ToString();
                string course_college1 = read1["course_college"].ToString();
                string teacher_id1 = read1["teacher_id"].ToString();
                string teacher_name1 = read1["teacher_name"].ToString();

                int count = 0;
                string campus = "";
                string sql3 = "select distinct xiaoqu from ele_Result where kch ='" + course_id1 + "' and jsh='" + teacher_id1 + "' and xn='" + _year + "' and xq='" + _semester + "'";
                SqlDataReader read3 = SQLHelper.ExecuteReader(sql3);
                while (read3.Read())
                {
                    count++;
                    campus = read3["xiaoqu"].ToString();
                }

                string sql2 = "select * from Rbook_message where course_id ='" + course_id1 + "' and date_year='" + _year + "' and semester='" + _semester + "' and state_id=4";
                SqlDataReader read2 = SQLHelper.ExecuteReader(sql2);
                for (; read2.Read(); )
                {
                    string book_id = read2["book_id"].ToString();
                    string book_name = read2["book_name"].ToString();
                    string book_catagory = read2["book_catagory"].ToString();
                    string book_editor = read2["book_editor"].ToString();
                    string book_price = read2["book_price"].ToString();
                    string press_name = read2["press_name"].ToString();
                    string press_time = read2["press_time"].ToString();//版次
                    string press_date = read2["press_date"].ToString();//出版时间

                    //从ele_Result中读取到校区的信息
                    if (count == 1)
                    {
                        string sql = "insert into Ebook_message values('" + _year + "','" + _semester + "','" + course_id1 + "','" + course_name1 + "','" + course_college1 + "','" + teacher_id1 + "','" + teacher_name1 + "','','','" + book_id + "','" + book_name + "','" + book_catagory + "','" + book_editor + "','" + book_price + "','" + press_name + "','" + press_time + "','" + press_date + "','0','" + campus + "',0)";
                        SQLHelper.ExecuteNonQuery(sql);
                    }
                    else
                    {
                        string sql = "insert into Ebook_message values('" + _year + "','" + _semester + "','" + course_id1 + "','" + course_name1 + "','" + course_college1 + "','" + teacher_id1 + "','" + teacher_name1 + "','','','" + book_id + "','" + book_name + "','" + book_catagory + "','" + book_editor + "','" + book_price + "','" + press_name + "','" + press_time + "','" + press_date + "','0','明故宫',0)";
                        SQLHelper.ExecuteNonQuery(sql);
                        string str = "insert into Ebook_message values('" + _year + "','" + _semester + "','" + course_id1 + "','" + course_name1 + "','" + course_college1 + "','" + teacher_id1 + "','" + teacher_name1 + "','','','" + book_id + "','" + book_name + "','" + book_catagory + "','" + book_editor + "','" + book_price + "','" + press_name + "','" + press_time + "','" + press_date + "','0','将军路',0)";
                        SQLHelper.ExecuteNonQuery(str);
                    }
                }
            }
        }
        //把之前从Excel中整理的东西删掉
        string sql4 = "delete from Ebook_message where get_id='9'";
        SQLHelper.ExecuteNonQuery(sql4);



        //第三步：删除上   三个   学期领过书的老师（隔一年领一回书的功能的实现代码）
        //以时间（学年学期）为标志，如果上个学年（或学期）有相关老师的记录且get_id='1'，表示上学期领过。。。
        //如果上学年（或学期）   没有相关老师这本书的信息   或者   get_id='0'，表示上学期没领过。。。
        {
            //无论学期是‘1’或‘2’，都要看上一学年的两个学期，所以下面的代码是公用的。。。。。。。。。。
            string first_year = "", second_year = "";
            string[] year_array = date_year.Split('-');
            first_year = year_array[0];
            second_year = year_array[1];
            first_year = (Convert.ToInt32(first_year) - 1).ToString();
            second_year = (Convert.ToInt32(second_year) - 1).ToString();
            string date_last_year = first_year + "-" + second_year;
            SqlDataReader reader0 = SQLHelper.ExecuteReader("select book_id,teacher_id,get_id from Ebook_message where date_year='" + date_year + "'");
            while (reader0.Read())
            {
                string book_id0 = reader0[0].ToString();
                string teacher_id0 = reader0[1].ToString();
                SqlDataReader reader1 = SQLHelper.ExecuteReader("select count(*) from Ebook_message where date_year='" + date_last_year + "' and book_id='" + book_id0 + "' and teacher_id='" + teacher_id0 + "'");
                while (reader1.Read())
                {
                    int num = Convert.ToInt32(reader1[0].ToString());
                    if (num == 0)//上学期（或学年）没有相关老师这本书的信息，不用删除本学期的记录，直接break；
                    {
                        break;
                    }
                    else//否则判断get_id
                    {
                        SqlDataReader reader2 = SQLHelper.ExecuteReader("select get_id from Ebook_message where date_year='" + date_last_year + "' and book_id='" + book_id0 + "' and teacher_id='" + teacher_id0 + "'");
                        while (reader2.Read())
                        {
                            string get_id2 = reader2[0].ToString();
                            if (get_id2 != "0")//如果领用，删除本学期的记录
                            {
                                SQLHelper.ExecuteNonQuery("delete from Ebook_message where date_year='" + date_year + "' and book_id='" + book_id0 + "' and teacher_id='" + teacher_id0 + "'");
                            }
                        }
                    }
                }
            }
        }
        if (semester.CompareTo("1") == 0)
        {
            //对于学期是‘1’，如2012-2013年第1学期，还要看10-11年第2学期。。。
            string first_year = "", second_year = "";
            string[] year_array = date_year.Split('-');
            first_year = year_array[0];
            second_year = year_array[1];
            first_year = (Convert.ToInt32(first_year) - 2).ToString();
            second_year = (Convert.ToInt32(second_year) - 2).ToString();
            string date_last_last_year = first_year + "-" + second_year;
            SqlDataReader reader0 = SQLHelper.ExecuteReader("select book_id,teacher_id from Ebook_message where date_year='" + date_year + "' and semester='" + semester + "'");
            while (reader0.Read())
            {
                string book_id0 = reader0[0].ToString();
                string teacher_id0 = reader0[1].ToString();
                SqlDataReader reader1 = SQLHelper.ExecuteReader("select count(*) from Ebook_message where date_year='" + date_last_last_year + "' and semester='2' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                while (reader1.Read())
                {
                    int num = Convert.ToInt32(reader1[0].ToString());
                    if (num == 0)//上学期（或学年）没有相关老师的信息，不用删除本学期的记录，直接break；
                    {
                        break;
                    }
                    else//否则判断get_id
                    {
                        SqlDataReader reader2 = SQLHelper.ExecuteReader("select get_id from Ebook_message where date_year='" + date_year + "' and semester='2' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                        while (reader2.Read())
                        {
                            string get_id2 = reader2[0].ToString();
                            if (get_id2 == "0")//如果领用，删除本学期的记录
                            {
                                SQLHelper.ExecuteNonQuery("delete from Ebook_message where date_year='" + date_year + "' and semester='1' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                            }
                        }
                    }
                }
            }
        }

        if (semester.CompareTo("2") == 0)
        {
            //对于学期是‘2’，如2012-2013年第2学期，还要看12-13年第1学期。。。
            SqlDataReader reader0 = SQLHelper.ExecuteReader("select teacher_id,book_id from Ebook_message where date_year='" + date_year + "' and semester='" + semester + "'");
            while (reader0.Read())
            {
                string teacher_id0 = reader0[0].ToString();
                string book_id0 = reader0[1].ToString();
                SqlDataReader reader1 = SQLHelper.ExecuteReader("select count(*) from Ebook_message where date_year='" + date_year + "' and semester='1' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                while (reader1.Read())
                {
                    int num = Convert.ToInt32(reader1[0].ToString());
                    if (num == 0)//上学期（或学年）没有相关老师的信息，不用删除本学期的记录，直接break；
                    {
                        break;
                    }
                    else//否则判断get_id
                    {
                        SqlDataReader reader2 = SQLHelper.ExecuteReader("select state_id from Ebook_message where date_year='" + date_year + "' and semester='1' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                        while (reader2.Read())
                        {
                            string get_id2 = reader2[0].ToString();
                            if (get_id2 == "0")//如果领用，删除本学期的记录
                            {
                                SQLHelper.ExecuteNonQuery("delete from Ebook_message where date_year='" + date_year + "' and semester='2' and teacher_id='" + teacher_id0 + "' and book_id='" + book_id0 + "'");
                            }
                        }
                    }
                }
            }
        }



        //第四步：将Ebook_message中state=0的转到Abook_message中
        string a_year = DropDownList1.SelectedValue;
        string a_semester = DropDownList2.SelectedValue;
        SqlDataReader aread = SQLHelper.ExecuteReader("select * from Ebook_message where date_year='" + a_year + "' and semester='" + a_semester + "' and state=0");
        for (; aread.Read(); )
        {
            string adate_year = aread["date_year"].ToString();
            string asemester = aread["semester"].ToString();
            string acourse_id = aread["course_id"].ToString();
            string acourse_name = aread["course_name"].ToString();
            string acourse_college = aread["course_college"].ToString();
            string acampus = aread["campus"].ToString();
            string ateacher_id = aread["teacher_id"].ToString();
            string ateacher_name = aread["teacher_name"].ToString();
            string abook_id = aread["book_id"].ToString();
            string abook_name = aread["book_name"].ToString();
            string abook_catagory = aread["book_catagory"].ToString();
            string abook_editor = aread["book_editor"].ToString();
            string abook_price = aread["book_price"].ToString();
            string apress_name = aread["press_name"].ToString();
            string apress_time = aread["press_time"].ToString();//版次
            string apress_date = aread["press_date"].ToString();//出版时间

            string sql = "insert into Abook_message (year,semester,course_id,course_name,course_college,campus,teacher_id,teacher_name,book_id,book_name,book_catagory,book_price,press_name,press_time,press_date,get_id,teacher_college,teacher_major,book_editor,state)";
            sql += "values('" + adate_year + "','" + asemester + "','" + acourse_id + "','" + acourse_name + "','" + acourse_college + "','" + acampus + "','" + ateacher_id + "','" + ateacher_name + "','" + abook_id + "','" + abook_name + "','" + abook_catagory + "','" + abook_price + "','" + apress_name + "','" + apress_time + "','" + apress_date + "','0','0','0','" + abook_editor + "',0)";

            SQLHelper.ExecuteNonQuery(sql);
        }
        //Ebook_message中统计过的state更改为1
        string ChangE = "Update Ebook_message set state=1 where state=0";
        SQLHelper.ExecuteNonQuery(ChangE);


        //第五步：将book_message中state=0的转到Abook_message中
         string BookMessageSql = "select * from book_message where state_id=4 and add_state_id=0 and date_year='" + _year + "' and semester='" + _semester + "'";
        SqlDataReader read4 = SQLHelper.ExecuteReader(BookMessageSql);
        while (read4.Read())
        {
            string id = read4["id"].ToString();
            string dy = read4["date_year"].ToString();
            string se = read4["semester"].ToString();
            string tc = read4["teacher_college"].ToString();
            string ti = read4["teacher_id"].ToString();
            string tn = read4["teacher_name"].ToString();
            string ci = read4["course_id"].ToString();
            string cn = read4["course_name"].ToString();
            string bi = read4["book_id"].ToString();
            string bn = read4["book_name"].ToString();
            string bp = read4["book_price"].ToString();
            string pn = read4["press_name"].ToString();
            string pt = read4["press_time"].ToString();
            string pd = read4["press_date"].ToString();
            string ua = read4["university_advise"].ToString();
            string si = read4["state_id"].ToString();
            string be = read4["book_editor"].ToString();
            string cp = read4["Campus"].ToString();

            string str = "insert into Abook_message values('" + dy + "','" + se + "','" + ci + "','" + cn + "',"
            +"'"+tc+"','" + ti + "','" + tn + "','" + tc + "','0',"
            +"'" + bi + "','" + bn + "','0','" + be + "','" + bp + "','" + pn + "','" + pt + "','" + pd + "','0','" + cp + "',0)";
            SQLHelper.ExecuteNonQuery(str);
        }

        //book_message中统计过的state更改为1
        string ChangB = "Update book_message set add_state_id=1 where add_state_id=0";
        SQLHelper.ExecuteNonQuery(ChangB);


        //第六步：Abook_message数量统计到book_demand
        string Ayear = DropDownList1.SelectedValue; ;
        string Asemester = DropDownList2.SelectedValue;
        SqlDataReader reader = SQLHelper.ExecuteReader("select distinct book_id from Abook_message where state=0 and year='" + Ayear + "' and semester='" + Asemester + "'");
        string  Abook_name = "", Abook_editor = "", Abook_price = "", Apress_name = "", Apress_time = "", Apress_date = "", Acampus = "";
        while (reader.Read())
        {
            string Abook_id = reader["book_id"].ToString();
            SqlDataReader reader4 = SQLHelper.ExecuteReader("select book_name,book_editor,book_price,press_name,press_time,press_date,campus from Abook_message where book_id='" + Abook_id + "' and state=0");
            while (reader4.Read())
            {
                Abook_name = reader4[0].ToString();
                Abook_editor = reader4[1].ToString();
                Abook_price = reader4[2].ToString();
                Apress_name = reader4[3].ToString();
                Apress_time = reader4[4].ToString();
                Apress_date = reader4[5].ToString();
                Acampus = reader4[6].ToString();
            }
            SqlDataReader reader3 = SQLHelper.ExecuteReader("select count(*) from Abook_message where state=0 and book_id='" + Abook_id + "' and year='" + Ayear + "' and semester='" + Asemester + "'");
            while (reader3.Read())
            {
                string sql = "insert into book_demand values('" + Ayear + "','" + Asemester + "','" + Abook_id + "','" + Abook_name + "','" + Abook_editor + "','" + Abook_price + "','" + reader3[0].ToString() + "','" + Acampus + "','" + Apress_name + "','" + Apress_time + "','" + Apress_date + "',0)";
                SQLHelper.ExecuteNonQuery(sql);
            }
        }
        //Abook_message中统计过的state更改为1
        string ChangA = "Update Abook_message set state=1 where state=0";
        SQLHelper.ExecuteNonQuery(ChangA);


        //第七步：将学生的需求信息Abook_message_stu整合到book_demand；
        SqlDataReader readstu = SQLHelper.ExecuteReader("select distinct BookID,campusName from Abook_message_stu where state=0");
        string ASbook_id = "", ASbook_name = "", ASbook_editor = "", ASbook_price = "", ASbook_number = "", ASpress_name = "", ASpress_time = "", AScampus = "";
        while (readstu.Read())
        {
            ASbook_id = readstu["BookID"].ToString();
            AScampus = readstu["campusName"].ToString();
            SqlDataReader readstu1 = SQLHelper.ExecuteReader("select BookName,Author,UnitPrice,Publish,Version from Abook_message_stu where BookID='" + ASbook_id + "' and campusName='" + AScampus + "' and state=0");
            while (readstu1.Read())
            {
                ASbook_name = readstu1["BookName"].ToString();
                ASbook_editor = readstu1["Author"].ToString();
                ASbook_price = readstu1["UnitPrice"].ToString();
                ASpress_name = readstu1["Publish"].ToString();
                ASpress_time = readstu1["Version"].ToString();
            }
            //数出这本书在Abook_message_stu中有几本
            string sql1 = "select count(*) from Abook_message_stu where BookID='" + ASbook_id + "' and campusName='" + AScampus + "' and state=0";
            SqlDataReader readstu2 = SQLHelper.ExecuteReader(sql1);
            while (readstu2.Read())
            {
                ASbook_number = readstu2[0].ToString();
            }
            //看这本书是否在book_demand没有形成订单的信息中已经有了
            string sql2 = "select count(*) from book_demand where book_id='" + ASbook_id + "' and campus='" + AScampus + "' and state_id=0";
            SqlDataReader rd = SQLHelper.ExecuteReader(sql2);
            string numd = "";
            while (rd.Read())
            {
                numd = rd[0].ToString();
            }
            if (numd == "0")//如果没有 新加入
            {
                string sql3 = "insert into book_demand values('" + Ayear + "','" + Asemester + "','" + ASbook_id + "','" + ASbook_name + "','" + ASbook_editor + "','" + ASbook_price + "','" + ASbook_number + "','" + AScampus + "','" + ASpress_name + "','" + ASpress_time + "','',0)";
                SQLHelper.ExecuteNonQuery(sql3);
            }
            else//如果有 更新
            {
                ASbook_number = (Convert.ToInt32(numd) + Convert.ToInt32(ASbook_number)).ToString();
                string sql3 = "update book_demand set book_number='" + ASbook_number + "' where date_year='" + Ayear + "' and semester='" + Asemester + "' and book_id='" + ASbook_id + "' and campus='" + AScampus + "' and state_id=0";
                SQLHelper.ExecuteNonQuery(sql3);
            }
        }
        //Abook_message_stu中统计过的state更改为1
        string ChangAS = "Update Abook_message_stu set state=1 where state=0";
        SQLHelper.ExecuteNonQuery(ChangAS);



        //第八步：弹出对话框提示生成成功
        WebMessageBox.Show("生成成功");
    }
}