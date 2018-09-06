using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task.Data;
using Task.Entity;
using System.Data.Entity;
using Moq;

namespace Task.Business.Tests
{
  [TestClass()]
  public class TaskManagerTests
  {
    //private TaskManager taskManager = new TaskManager();

    [TestMethod()]
    public void FindAllTest()
    {
            IQueryable<Task.Entity.TaskDetail> Tasks = new List<TaskDetail>
            {
                new TaskDetail
                {
                    
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 20,
                    StartDate = DateTime.Now,
                    TaskName = "Task1"
                },
                new TaskDetail
                {
                    
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 30,
                    StartDate = DateTime.Now,
                    TaskName = "Task2"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TaskDetail>>();
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Provider).Returns(Tasks.Provider);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(Tasks.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(Tasks.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(Tasks.GetEnumerator());

            var mockContext = new Mock<TaskManagerContext>();
            mockContext.Setup(c => c.TaskDetail).Returns(mockSet.Object);

            // Act - fetch Task
            var repository = new TaskManager(mockContext.Object);
            var actual = repository.FindAll();

            
            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual("Task2", actual.ElementAt(1).TaskName);
            Assert.IsNotNull(actual);
            Assert.AreEqual(30, actual.ElementAt(1).Priority);
           
        }

        [TestMethod()]
    public void FindTest()
    {
            IQueryable<Task.Entity.TaskDetail> Tasks = new List<TaskDetail>
            {
                new TaskDetail
                {
                    TaskID = 1,
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 20,
                    StartDate = DateTime.Now,
                    TaskName = "Task1"
                },
                new TaskDetail
                {
                    TaskID = 2,
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 30,
                    StartDate = DateTime.Now,
                    TaskName = "Task2"
                }
            }.AsQueryable();

            var mockSet = new Mock<DbSet<TaskDetail>>();
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Provider).Returns(Tasks.Provider);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(Tasks.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(Tasks.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(Tasks.GetEnumerator());

            var mockContext = new Mock<TaskManagerContext>();
            mockContext.Setup(c => c.TaskDetail).Returns(mockSet.Object);

            // Act - fetch books
            var repository = new TaskManager(mockContext.Object);
            var actual = repository.Find(1);

            Assert.AreEqual(1, actual.TaskID);
            Assert.AreEqual("Task1", actual.TaskName);
        }

    [TestMethod()]
    public void SaveTaskTest()
    {
            IQueryable<Task.Entity.TaskDetail> Tasks = new List<TaskDetail>
            {
                new TaskDetail
                {
                    TaskID = 1,
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 20,
                    StartDate = DateTime.Now,
                    TaskName = "Task1"
                },
                new TaskDetail
                {
                    TaskID = 2,
                    EndDate = DateTime.Now,
                    IsActive= "Yes",
                    ParentTask = null,
                    ParentTaskID = null,
                    Priority = 30,
                    StartDate = DateTime.Now,
                    TaskName = "Task2"
                }
            }.AsQueryable();

            IQueryable<Task.Entity.ParentTask> TasksParent = new List<ParentTask>
            {
                new ParentTask
                {
                    ParentTaskID = 1,
                    ParentTaskName = "P1"
                },
                new ParentTask
                {
                    ParentTaskID = 1,
                    ParentTaskName = "P1"
                }
            }.AsQueryable();

            //Setting up TaskDTO
            TaskDTO tskDTO = new TaskDTO()
            {
                EndDate = DateTime.Now,
                IsActive = "Yes",
                ParentTaskName = "Parent Task",
                Priority = 21,
                StartDate = DateTime.Now,
                TaskID = 0,
                TaskName = "Task1"
            };

           

            /// Arrange - We're mocking our dbSet & dbContext
            // in-memory implementations of you context and sets
            var mockSet = new Mock<DbSet<TaskDetail>>();
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Provider).Returns(Tasks.Provider);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.Expression).Returns(Tasks.Expression);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.ElementType).Returns(Tasks.ElementType);
            mockSet.As<IQueryable<TaskDetail>>().Setup(m => m.GetEnumerator()).Returns(Tasks.GetEnumerator());


            var mockSetParent = new Mock<DbSet<ParentTask>>();
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(TasksParent.Provider);
            //mockSet.As<IQueryable<ParentTask>>().Setup(m => m.Provider).Returns(() => TasksParent.Provider);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.Expression).Returns(TasksParent.Expression);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.ElementType).Returns(TasksParent.ElementType);
            mockSet.As<IQueryable<ParentTask>>().Setup(m => m.GetEnumerator()).Returns(TasksParent.GetEnumerator());



            var mockContext = new Mock<TaskManagerContext>();
            mockContext.Setup(m => m.TaskDetail).Returns(mockSet.Object);
            mockContext.Setup(m => m.ParentTask).Returns(mockSetParent.Object);


            //mockContext.Setup(c => c.TaskDetail).Returns(() => mockSet.Object);
            //mockContext.Setup(c => c.ParentTask).Returns(() => mockSetParent.Object);

            // Act - Add the book
            var repository = new TaskManager(mockContext.Object);
            repository.SaveTask(tskDTO);

            // Assert
            // These two lines of code verifies that a book was added once and
            // we saved our changes once.
            mockSet.Verify(m => m.Add(It.IsAny<TaskDetail>()), Times.Once);
            mockContext.Verify(m => m.SaveChanges(), Times.Once);

        }

    [TestMethod()]
    public void EndTaskByIDTest()
    {
      Assert.Fail();
    }

    

  }
}