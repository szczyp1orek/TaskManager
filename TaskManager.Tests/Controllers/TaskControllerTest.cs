using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskManager;
using TaskManager.Controllers;
using TaskManager.Models;


namespace TaskManager.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded. 
            //Make sure the provider assembly is available to the running application. 
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        TaskManagerEntities taskDB = new TaskManagerEntities();

        [TestMethod]
        public void Index()
        {
            //arrange
            TaskController controller = new TaskController();
            //act
            ViewResult result=controller.Index() as ViewResult;
            //assert
            Assert.AreEqual("Index", result.ViewName);
        }
        [TestMethod]
        public void Details()
        {
            //arrange
            TaskController controller = new TaskController();
            //act
            var result = controller.Details(1) as ViewResult;
            var task = (MTask)result.ViewData.Model;
            //assert
            Assert.AreEqual("Frontier Report",task.Name);
        }
        [TestMethod]
        public void Create()
        {
            //arrange
            TaskController controller = new TaskController();
            var testTask = new MTask(){Name="nazwa", EmployeeId=1, IsDone=true,Date =new DateTime(2016,12,12) , Description="task for testing"};
            //act
            var result = controller.Create(testTask) as ViewResult ;
            //var task = (MTask)result.ViewData.Model;
            //assert
            Assert.AreEqual(testTask, result.Model);
        }
        [TestMethod]
        public void Delete()
        {
            //arrange
            TaskController controller = new TaskController();
           
            var result = controller.DeleteConfirm(1) as ViewResult;
            //assert
            Assert.AreEqual("Index", result.ViewName);
        }
    }
        
    
}
