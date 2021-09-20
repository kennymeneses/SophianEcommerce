﻿using Ecommerce.DataAccess;
using Ecommerce.Models.Entities;
using Ecommerce.Models.InputsBody;
using Ecommerce.Models.ResponseStatus;
using Ecommerce.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.BussinessLayer.UserManagment
{
    public class UserManager : IUserManager
    {
        DataContext _context;
        ILogger<UserManager> _logger;
        EncryptData _encrypt;

        public UserManager(DataContext context, ILogger<UserManager> logger, EncryptData encryptData)
        {
            _context = context;
            _logger = logger;
            _encrypt = encryptData;
        }

        public async Task<DataQuery> Search(int page, int size)
        {
            DataQuery dataQuery = new DataQuery();
            List<User> result = new List<User>();
            DateTime localDate = DateTime.Now;

            try
            {
                var timer = Stopwatch.StartNew();

                _logger.LogInformation("a GET request to UserController started at {0}", localDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                result = await _context.users.Where(x=>x.removed==false).ToListAsync();

                timer.Stop();

                if (result.Count == 0)
                {
                    _logger.LogInformation("The user search did not found any user.");

                    dataQuery.data = null;
                    dataQuery.apiMessage = String.Format("The user search did not found any user.");
                    dataQuery.total = 0;

                    return dataQuery;
                }

                var resultFilter = result.Select(u => new
                {
                    u.userId,
                    u.nickUser,
                    u.namesUser,
                    u.surnamesUser,
                    u.dniUser,
                    u.roleId,
                    u.emailUser,
                    u.ageUser,
                    u.genderUser,
                    u.creationDate
                }).ToList();

                var listUser = resultFilter.ConvertAll(x => (object)x);

                _logger.LogInformation("The user search found {0} users and took: {0} ms.", listUser.Count(), timer.ElapsedMilliseconds);

                dataQuery.data = listUser;
                dataQuery.apiMessage = String.Format("Was found {0} users.", listUser.Count());
                dataQuery.total = listUser.Count();

                return dataQuery;
            }
            catch (Exception ex)
            {
                dataQuery.apiMessage = ex.Message;
                dataQuery.data = null;
                dataQuery.total = 0;
                dataQuery.apiStatus = "error";

                return dataQuery;
            }
        }

        public async Task<SingleQuery> SearchById(int id)
        {
            var singleQuery = new SingleQuery();
            var user = new User();
            DateTime localDate = DateTime.Now;

            try
            {                
                _logger.LogInformation("A GET request to find the user with id {0} started at {1}",id, localDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                var timer = Stopwatch.StartNew();

                user = await _context.users.FirstOrDefaultAsync(u => u.userId == id);

                timer.Stop();

                if (user == null)
                {
                    _logger.LogError("The user does not exist");

                    singleQuery.apiStatus = "error";
                    singleQuery.apiMessage = String.Format("The user with id {0} was not found.", id);
                    singleQuery.entity = null;

                    return singleQuery;
                }

                _logger.LogInformation("The user was found and took {0} ms.",timer.ElapsedMilliseconds);

                singleQuery.apiStatus = "ok";
                singleQuery.entity = user;
                singleQuery.apiMessage = String.Format("The user with id {0} was found",id);

                return singleQuery;
            }

            catch (Exception ex)
            {
                _logger.LogError("An error ocurred: {0}", ex.Message);

                throw;
            }
        }

        public SingleQuery SearchByNameUseID(int value)

        {
            var singleQuery = new SingleQuery();
            var user = new User();

            try
            {
                _logger.LogInformation("the search of the name from a user started");
                user = _context.users.FirstOrDefault(a => a.userId == value);

                if(user == null) 
                
                {
                    _logger.LogError("The user does not exist");

                    singleQuery.apiStatus = "error";
                    singleQuery.entity = null;
                    singleQuery.apiMessage = string.Format("the user with Id does not exist");

                    return singleQuery;

                }

                _logger.LogInformation("user found");

                singleQuery.apiStatus = "ok";
                singleQuery.entity = user;
                singleQuery.apiMessage = String.Format("the name of the user found is: {0}", user.namesUser);

                return singleQuery;

            }
            catch (Exception ex)
            {
                _logger.LogError("something went wrong, please try again{0}", ex.Message);
                throw;
            }
        }

        public async Task<CheckStatus> Create(BaseInputEntity entity)
        {
            var checkStatus = new CheckStatus();
            var userInput = (UserInput)entity;
            var user = new User();
            var usersPass = new UserPass();
            var userCreated = new User();
            DateTime localDate = DateTime.Now;

            user.nickUser = userInput.nick;
            user.namesUser = userInput.namesUser;
            user.surnamesUser = userInput.surnameUser;
            user.dniUser = userInput.numberDocumentUser;
            user.roleId = userInput.rolIdUser;
            user.emailUser = userInput.emailUser;
            user.ageUser = userInput.ageUser;
            user.genderUser = userInput.genderUser;
            user.dataCreated = userInput.dateCreation;
            user.removed = userInput.removed;

            try
            {                
                _logger.LogInformation("A POST request to user controller started at: {0}", localDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                var timer = Stopwatch.StartNew();

                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();

                checkStatus.apiMessage = string.Format("The user {0} was create succesfully.", user.nickUser);
                checkStatus.code = "201";
                checkStatus.apiStatus = "ok";

                userCreated = await _context.users.FirstOrDefaultAsync(u => u.nickUser == userInput.nick);

                if (userCreated != null)
                {
                    usersPass.userId = userCreated.userId;
                    usersPass.passwordHash = _encrypt.PasswordToHash(userInput.passwordUser);

                    await _context.userPass.AddAsync(usersPass);

                    await _context.SaveChangesAsync();

                    timer.Stop();

                    _logger.LogInformation("The user was create succesfully with id: {0} an took: {1} ms.", userCreated.userId, timer.ElapsedMilliseconds);
                }
            }
            catch (Exception ex)
            {
                checkStatus.code = "501";
                checkStatus.apiStatus = "error";
                checkStatus.apiMessage = String.Format("The user was not create: {0} \r\n For more details: {1}", ex.Message, ex.InnerException);

                _logger.LogError("The user was not create");
            }

            return checkStatus;            
        }

        public async Task<CheckStatus> Update(int id, BaseInputEntity entity)
        {
            var status = new CheckStatus();
            var userInput = (UserInput)entity;
            var user = new User();
            var userPass = new UserPass();
            DateTime localDate = DateTime.Now;

            try
            {
                _logger.LogInformation("A UPDATE request to find the user with id {0} started at: {1}",id, localDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                var timer = Stopwatch.StartNew();

                user = await _context.users.FirstOrDefaultAsync(u => u.userId == id);

                if(user == null)
                {
                    status.code = "404";
                    status.apiStatus = "error";
                    status.apiMessage = String.Format("The user was not found.");
                    status.id = 200;

                    _logger.LogInformation("The user with id {0} was not found.");
                }

                    user.nickUser = userInput.nick;
                    user.namesUser = userInput.namesUser;
                    user.surnamesUser = userInput.surnameUser;
                    user.dniUser = userInput.numberDocumentUser;
                    user.roleId = userInput.rolIdUser;
                    user.emailUser = userInput.emailUser;
                    user.ageUser = userInput.ageUser;
                    user.genderUser = userInput.genderUser;
                    user.dataCreated = userInput.dateCreation;
                    user.removed = userInput.removed;

                    await _context.SaveChangesAsync();

                    timer.Stop();

                    status.apiStatus = "ok";
                    status.apiMessage = String.Format("The user with id {0} was update.",id);
                    status.code = "200";
                    status.id = 200;

                    _logger.LogInformation("The user with id {0} was update succesfully and took : {1} ms.", id, timer.ElapsedMilliseconds);                
            }

            catch (Exception ex)
            {
                status.code = "501";
                status.apiStatus = "error";
                status.apiMessage = String.Format("The user was not create: {0} \r\n For more details: {1}", ex.Message, ex.InnerException);

                _logger.LogError("The user was not create");
            }

            return status;
        }

        public async Task<CheckStatus> Remove(int id)
        {
            var status = new CheckStatus();
            var user = new User();
            DateTime localDate = DateTime.Now;

            try
            {
                _logger.LogInformation("A DELETE request to find the user with id {0} started", id);

                var timer = Stopwatch.StartNew();

                user = await _context.users.FirstOrDefaultAsync(u => u.userId == id);

                if(user == null)
                {
                    status.code = "404";
                    status.apiStatus = "error";
                    status.apiMessage = String.Format("The user was not found");
                    status.id = 404;

                    _logger.LogInformation("The user with id {0} was not found");

                    return status;
                }

                user.removed = true;

                await _context.SaveChangesAsync();

                timer.Stop();

                status.code = "200";
                status.apiStatus = "ok";
                status.apiMessage = String.Format("The user was removed");
                status.id = 200;

                _logger.LogInformation("The user with id {0} was removed and took {1} ms.", user.userId, timer.ElapsedMilliseconds);
            }
            catch (Exception ex)
            {
                status.code = "501";
                status.apiStatus = "error";
                status.apiMessage = String.Format("The user was found: {0} \r\n For more details: {1}", ex.Message, ex.InnerException);

                throw;
            }

            return status;
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
        
    }
}