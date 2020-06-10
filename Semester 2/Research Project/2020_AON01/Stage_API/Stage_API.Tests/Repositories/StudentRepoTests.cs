using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Stage_API.Business.Models;
using Stage_API.Data;
using Stage_API.Data.Repositories;
using Stage_API.Domain.Classes;
using System.Collections.Generic;
using System.Linq;

namespace Stage_API.Tests.Repositories
{
    public class StudentRepoTests
    {

        private readonly StageContext _context = TestHelper.Context;
        private StudentRepository _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new StudentRepository(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.DetachEntries();
        }

        [Test]
        public void GetStudentWithWrongId_100_Returns_Null()
        {
            var student = _repository.GetById(100);
            Assert.Null(student);
        }

        [Test]
        public void GetStudentWithId1_Returns_CorrectStudentIncludingFavorieteOpdracht()
        {
            var student = _repository.GetById(6, s => s.FavorieteOpdrachten);
            Assert.AreEqual(6, student.Id);
            Assert.NotNull(student.FavorieteOpdrachten);
            Assert.IsNotEmpty(student.FavorieteOpdrachten);
            Assert.AreEqual(4, student.FavorieteOpdrachten.ElementAt(0).StagevoorstelId);
        }

        [Test]
        public void GetStudentWithId1_Returns_CorrectStudentIncludingToegewezenOpdracht()
        {
            var student = _repository.GetById(6, s => s.ToegewezenStageOpdracht);
            Assert.AreEqual(6, student.Id);
            Assert.NotNull(student.ToegewezenStageOpdracht);
            Assert.AreEqual(2, student.ToegewezenStageOpdracht.Id);
        }

        [Test]
        public void PutStudent_CorrectlyUpdateStudent()
        {
            //Arrange
            var student = _context.Studenten.AsNoTracking().First(s => s.Id == 7);
            student.Voornaam = "T-Rex";

            //Act
            _repository.Update(7, student);
            var updatedStudent = _context.Studenten.Find(7);

            //Assert
            Assert.AreEqual("T-Rex", updatedStudent.Voornaam);
        }

        [Test]
        public void PutStudent_ClearAllFavorites_WorksCorrectly()
        {
            //Arrange
            var student = _context.Studenten.Where(s => s.Id == 9)
                .Include(s => s.FavorieteOpdrachten).AsNoTracking().FirstOrDefault();

            var studentModel = new StudentModel { Id = 9, FavorieteOpdrachten = new List<StagevoorstelModel>() };


            //Act
            _repository.UpdateFavorieten(9, studentModel);

            //Assert
            var updatedStudent = _context.Studenten.Where(s => s.Id == 9)
                .Include(s => s.FavorieteOpdrachten).FirstOrDefault();

            Assert.NotNull(updatedStudent);
            Assert.NotNull(updatedStudent.FavorieteOpdrachten);
            Assert.IsEmpty(updatedStudent.FavorieteOpdrachten);
        }

        [Test]
        public void PutStudent_AddNewFavorites_WorksCorrectly()
        {
            //Arrange
            var student = _context.Studenten.Where(s => s.Id == 8)
                .Include(s => s.FavorieteOpdrachten).AsNoTracking().FirstOrDefault();

            var studentModel = new StudentModel
            {
                Id = 8,
                FavorieteOpdrachten = student.FavorieteOpdrachten.Select(fav => new StagevoorstelModel { Id = fav.StagevoorstelId }).ToList()
            };

            var count = student.FavorieteOpdrachten.Count;
            var voorstelToAdd = new StagevoorstelModel { Id = 8 };

            //Act
            studentModel.FavorieteOpdrachten.Add(voorstelToAdd);
            _repository.UpdateFavorieten(8, studentModel);

            //Assert
            var updatedStudent = _context.Studenten.Where(s => s.Id == 8)
                .Include(s => s.FavorieteOpdrachten).FirstOrDefault();
            Assert.NotNull(updatedStudent);
            Assert.NotNull(updatedStudent.FavorieteOpdrachten);
            Assert.AreEqual(count + 1, updatedStudent.FavorieteOpdrachten.Count);

            //Assert that favorite stagevoorstel added is the correct stagevoorstel.
            Assert.AreEqual(voorstelToAdd.Id, updatedStudent.FavorieteOpdrachten.ElementAt(1).StagevoorstelId);

        }

        [Test]
        public void PutStudent_ChangeFavorites_WorksCorrectly()
        {
            //Arrange
            var student = _context.Studenten.Where(s => s.Id == 7)
                .Include(s => s.FavorieteOpdrachten).First();

            var studentModel = new StudentModel
            {
                Id = 7,
                FavorieteOpdrachten = student.FavorieteOpdrachten.Select(fav => new StagevoorstelModel { Id = fav.StagevoorstelId }).ToList()
            };

            var count = student.FavorieteOpdrachten.Count;
            var voorstelToAdd = new StagevoorstelModel { Id = 8 };

            //Act
            studentModel.FavorieteOpdrachten = new List<StagevoorstelModel>() { voorstelToAdd };
            _repository.UpdateFavorieten(7, studentModel);

            //Assert
            var updatedStudent = _context.Studenten.Where(s => s.Id == 7)
                .Include(s => s.FavorieteOpdrachten).FirstOrDefault();
            Assert.NotNull(updatedStudent);
            Assert.NotNull(updatedStudent.FavorieteOpdrachten);

            Assert.AreEqual(count, updatedStudent.FavorieteOpdrachten.Count);
            //Assert that favorite stagevoorstel is the correct stagevoorstel.
            Assert.AreEqual(voorstelToAdd.Id, updatedStudent.FavorieteOpdrachten.ElementAt(0).StagevoorstelId);
        }


        [Test]
        public void PutStudent_Assign_WorksCorrectly()
        {
            //Arrange
            var student = _context.Studenten.Where(s => s.Id == 7)
                .Include(s => s.ToegewezenStageOpdracht).AsNoTracking().FirstOrDefault();

            var studentModel = new StudentModel { Id = 7, ToegewezenStageOpdracht = new Stagevoorstel { Id = 3 } };

            //Act
            _repository.UpdateToegewezen(7, studentModel);

            //Assert
            var updatedStudent = _context.Studenten.Where(s => s.Id == 7)
                .Include(s => s.ToegewezenStageOpdracht).AsNoTracking().FirstOrDefault();

            Assert.NotNull(updatedStudent);
            Assert.NotNull(updatedStudent.ToegewezenStageOpdracht);
            Assert.AreEqual(3, updatedStudent.ToegewezenStageOpdracht.Id);
        }


        [Test]
        public void PutStudent_Assign_ReturnsFalseIfAlreadyAssigned()
        {
            //Arrange
            var student = _context.Studenten.Where(s => s.Id == 9)
                .Include(s => s.ToegewezenStageOpdracht).AsNoTracking().FirstOrDefault();

            var studentModel = new StudentModel { Id = 9, ToegewezenStageOpdracht = new Stagevoorstel { Id = 1 } };

            //Act & Assert
            Assert.False(_repository.UpdateToegewezen(9, studentModel));
        }

    }
}