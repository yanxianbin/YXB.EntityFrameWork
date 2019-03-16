using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YXB.EntityFrameWork.Core;
using YXB.EntityFrameWork.Repository;

namespace YXB.EntityFrameWork.Tests
{

    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class TestMysql
    {
        [TestMethod]
        public void TestConnection()
        {
            var repository = Repository<UserInfo>.Instance.Insert(new UserInfo {
                DisplayName = "颜显斌"+DateTime.Now.ToString("yyyyMMddHHmmss"),
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            });
            var list = new List<UserInfo>()
            {
                new UserInfo {
                DisplayName = "颜显斌1",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌2",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌3",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌4",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌5",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌6",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            },
                new UserInfo {
                DisplayName = "颜显斌7",
                UserName="yanxianbin",
                BirthDay=new DateTime(1989,12,4),
                Address="湖南省茶陵县",
                Sex=Core.DataEnum.SexEnum.Boy,
                PassWord="123456",
                Remark="颜显斌测试用",
                IsLock="否",
                TellPhone="18007331866"
            }
            };
            Repository<UserInfo>.Instance.InsertBatch(list);
        }
    }
}
