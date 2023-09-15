using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Statics;
using FPT_Education_IC.ViewModels;
using System;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace FPT_Education_IC.Service
{
    public class ProgramsService
    {
        private readonly DataContext _context;

        public ProgramsService(DataContext context)
        {
            _context = context;
        }

        public int AddNewProgram(int exchangeData, string programCategorize, string programCountry, string programTitle, string participants, string facebookLink, DateTime startDate,
                                  DateTime endDate, DateTime registStDate, DateTime registEndDate, string paymentValue, string paymentDesc, DateTime paymentStDate,
                                  DateTime paymentEndDate, string programDescValue, string programImagePath, int userId, DateTime updateDate)
        {
            Models.Program program = new Models.Program();
            program.Type = "OUT"; // outbound
            program.Categorize = programCategorize;
            program.IsStudyExchange = exchangeData;
            program.UserId = userId;
            if (!string.IsNullOrEmpty(programCountry))
            {
                program.Country = programCountry;
            }
            program.Title = programTitle;
            program.Description = programDescValue;
            program.Participants = participants;
            program.StartDate = startDate;
            program.EndDate = endDate;
            program.FaceBookLink = facebookLink;
            program.RegisterStartDate = registStDate;
            program.RegisterEndDate = registEndDate;
            if (!string.IsNullOrEmpty(paymentValue))
            {
                program.PaymentValue = decimal.Parse(paymentValue);
                program.PaymentDescription = paymentDesc;
                program.PaymentStartDate = paymentStDate;
                program.PaymentEndDate = paymentEndDate;
            }
            program.Image = programImagePath;
            program.Status = StaticsData.CREATE_STATUS;
            program.UpdateUser = userId;
            program.UpdateDate = updateDate;

            _context.Programs.Add(program);
            _context.SaveChanges();

            // Return the ID of the newly added program
            return program.ProgramId;
        }

        public int UpdateProgram(int programID, int exchangeData, string programCategorize, string programCountry, string programTitle, string participants, string facebookLink, DateTime startDate,
                                 DateTime endDate, DateTime registStDate, DateTime registEndDate, string paymentValue, string paymentDesc, DateTime paymentStDate,
                                 DateTime paymentEndDate, string programDescValue, string programImagePath, int userId, DateTime updateDate)
        {
            Models.Program program = _context.Programs.FirstOrDefault(p => p.ProgramId == programID);
            if (program != null)
            {
                program.Categorize = programCategorize;
                program.IsStudyExchange = exchangeData;
                program.UserId = userId;
                if (!string.IsNullOrEmpty(programCountry))
                {
                    program.Country = programCountry;
                }
                program.Title = programTitle;
                program.Description = programDescValue;
                program.Participants = participants;
                program.StartDate = startDate;
                program.EndDate = endDate;
                program.FaceBookLink = facebookLink;
                program.RegisterStartDate = registStDate;
                program.RegisterEndDate = registEndDate;
                if (!string.IsNullOrEmpty(paymentValue))
                {
                    program.PaymentValue = decimal.Parse(paymentValue);
                    program.PaymentDescription = paymentDesc;
                    program.PaymentStartDate = paymentStDate;
                    program.PaymentEndDate = paymentEndDate;
                }
                if (!string.IsNullOrEmpty(programImagePath))
                {
                    program.Image = programImagePath;
                }
                program.UpdateUser = userId;
                program.UpdateDate = updateDate;

                _context.Programs.Update(program);
                _context.SaveChanges();
            }
            // Return the ID of the newly added program
            return programID;
        }

        public void AddPartnerProgram(int programId, List<string> partnerInfoList)
        {
            if (partnerInfoList.Count <= 0) return;

            for (int i = 0; i <  partnerInfoList.Count; i++)
            {
                ProgramCooperation cooperation = new ProgramCooperation();
                cooperation.ProgramId = programId;
                List<string> partnerElements = partnerInfoList[i].ToString().Split('|').ToList();
                cooperation.PartnerId = int.Parse(partnerElements[0]);
                cooperation.PartnerContact = int.Parse(partnerElements[2]);
                cooperation.MaxNumberRegister = int.Parse(partnerElements[3]);
                cooperation.NumberOfRegister = 0;
                _context.ProgramCooperations.Add(cooperation);
                _context.SaveChanges();
            }
        }

        public void AddProgramManageList(int programId, List<string> manageProgramList)
        {

        }

        public void UpdatePartnerProgram(int programId, List<string> partnerInfoList)
        {
            var result = _context.ProgramCooperations.Where(p => p.ProgramId == programId).ToList();
            if (result != null && result.Count > 0)
            {
                foreach (ProgramCooperation pc in result)
                {
                    _context.Remove(pc);
                    _context.SaveChanges();
                }
            }

            if (partnerInfoList.Count <= 0) return;

            AddPartnerProgram(programId, partnerInfoList);
        }

        public void AddResgisterQuestionProgram(int programId, int questionType, List<string> questionList)
        {
            if (questionList.Count <= 0) return;

            for (int i = 0; i < questionList.Count; i++)
            {
                RegisterQuestion question = new RegisterQuestion();
                question.ProgramId = programId;
                question.QuestionType = questionType;
                question.QuestionContent = questionList[i].ToString();
                question.IsRequire = false;
                _context.RegisterQuestions.Add(question);
                _context.SaveChanges();
            }
        }

        public ArrayList listProgramRequest(int userId)
        {
            ArrayList listProgram = new ArrayList();
            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(p => p.UserId == userId);
            if (usrAccount != null)
            {
                if (usrAccount.Role.Equals(StaticsData.STAFF_ROLE))
                {
                    var result = _context.Programs.Where(p => p.UserId == userId && p.Status == StaticsData.CREATE_STATUS).OrderByDescending(p => p.UpdateDate).ToList();
                    if (result != null && result.Count > 0)
                    {
                        listProgram.AddRange(result);
                    }
                }
                else if (usrAccount.Role.Equals(StaticsData.ADMIN_ROLE) || usrAccount.Role.Equals(StaticsData.SUPER_ADMIN_ROLE))
                {
                    var result = _context.Programs.Where(p => p.Status == StaticsData.CREATE_STATUS).OrderByDescending(p => p.UpdateDate).ToList();
                    if (result != null && result.Count > 0)
                    {
                        listProgram.AddRange(result);
                    }
                }
            }

            return listProgram;
        }

        public ArrayList GetAllManageProgram(int userId)
        {
            AutoChangeProgramStatus();
            ArrayList listProgram = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(p => p.UserId == userId);
            if (usrAccount != null)
            {
                var programResults = (
                    from p in _context.Programs
                    where p.UserId == userId
                    select p
                ).ToList();

                var managementResults = (
                    from p in _context.Programs
                    join pm in _context.ProgramManagements on p.ProgramId equals pm.ProgramId
                    where pm.UserId == userId
                    select p
                ).ToList();

                var result = programResults.Union(managementResults).ToList();
                if (result != null && result.Count > 0)
                {
                    listProgram.AddRange(result);
                }
            }
            return listProgram;
        }

        public ArrayList GetAllManageParticipatingProgram(int userId)
        {
            ArrayList listProgram = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(p => p.UserId == userId);
            if (usrAccount != null)
            {
                var programResults = (
                    from p in _context.Programs
                    where p.UserId == userId && p.Status != StaticsData.CLOSE_STATUS && p.Status != StaticsData.CREATE_STATUS
                    select p
                ).ToList();

                var managementResults = (
                    from p in _context.Programs
                    join pm in _context.ProgramManagements on p.ProgramId equals pm.ProgramId
                    where pm.UserId == userId && p.Status != StaticsData.CLOSE_STATUS && p.Status != StaticsData.CREATE_STATUS
                    select p
                ).ToList();

                var result = programResults.Union(managementResults).ToList();
                if (result != null && result.Count > 0)
                {
                    listProgram.AddRange(result);
                }
            }
            return listProgram;
        }

        public ArrayList GetAllManageParticipationProgram(int userId)
        {
            ArrayList listProgram = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(p => p.UserId == userId);
            if (usrAccount != null)
            {
                var programResults = (
                    from p in _context.Programs
                    where p.UserId == userId && p.Status == StaticsData.CLOSE_STATUS
                    select p
                ).ToList();

                var managementResults = (
                    from p in _context.Programs
                    join pm in _context.ProgramManagements on p.ProgramId equals pm.ProgramId
                    where pm.UserId == userId && p.Status == StaticsData.CLOSE_STATUS
                    select p
                ).ToList();

                var result = programResults.Union(managementResults).ToList();
                if (result != null && result.Count > 0)
                {
                    listProgram.AddRange(result);
                }
            }
            return listProgram;
        }


        public ArrayList listProgramCreateByUser(int userId)
        {
            ArrayList listProgram = new ArrayList();
            var result = _context.Programs.Where(p => p.UserId == userId).OrderByDescending(p => p.UpdateDate).ToList();
            if (result != null && result.Count > 0)
            {
                listProgram.AddRange(result);
            }

            return listProgram;
        }

        public ArrayList listQuestionRegisterProgram(int programId)
        {
            ArrayList listQuestion = new ArrayList();

            var query = _context.RegisterQuestions.Where(p => p.ProgramId == programId);

            var result = query.Select(c => new RegisterQuestion
            {
                Id = c.Id,
                ProgramId = programId,
                QuestionType = c.QuestionType,
                QuestionContent = c.QuestionContent,
                IsRequire = c.IsRequire

            })
                .OrderBy(c => c.Id)
                .ToList();

            if (result != null && result.Count > 0)
            {
                listQuestion.AddRange(result);
            }
            return listQuestion;
        }

        public RegisterAnswer GetRegisterAnswer(int usrId, int questionId)
        {
            RegisterAnswer registerAnswer = new RegisterAnswer();

            var query = _context.RegisterAnswers.FirstOrDefault(p => p.RegisterId == usrId && p.QuestionId == questionId);
            if (query != null)
            {
                registerAnswer = query;
            }
            return registerAnswer;
        }

        public void AddSubjectToStudyProgram(int programId, List<string> subjectList)
        {
            if (subjectList.Count <= 0) return;

            for (int i = 0; i < subjectList.Count; i++)
            {
                StudyExchange exchange = new StudyExchange();
                exchange.ProgramId = programId;
                List<string> subjectElements = subjectList[i].ToString().Split('|').ToList();
                exchange.CourseName = subjectElements[0];
                exchange.FptCourse = subjectElements[1];
                exchange.MaxMark = int.Parse(subjectElements[2]);
                exchange.PassMark = int.Parse(subjectElements[3]);
                _context.StudyExchanges.Add(exchange);
                _context.SaveChanges();
            }
        }

        public decimal GetPassMarkOfExchange(int exchangId)
        {
            decimal passMark = 0;
            StudyExchange studyExchange = _context.StudyExchanges.FirstOrDefault(p => p.ExchangeId == exchangId);
            if (studyExchange != null) passMark = studyExchange.PassMark;
            return passMark;
        }

        public void UpdateRegisterStudy(int exchangId, List<string> registerStudytList, int userUpdate)
        {
            if (registerStudytList.Count <= 0 || exchangId < 0) return;

            decimal passMark = GetPassMarkOfExchange(exchangId);
            for (int i = 0; i < registerStudytList.Count; i++)
            {
                List<string> registerStudyElements = registerStudytList[i].ToString().Split('|').ToList();
                int userId = int.Parse(registerStudyElements[0]);
                decimal resultMark = decimal.Parse(registerStudyElements[1]);
                bool condition = true;
                if(string.IsNullOrEmpty(registerStudyElements[2]) || "false".Equals(registerStudyElements[2]))
                {
                    condition = false;
                }
                
                string conditionReason = registerStudyElements[3];

                StudyResult studyResult = _context.StudyResults.FirstOrDefault(p => p.ExchangeId == exchangId && p.UserId == userId);


                if (studyResult != null)
                {
                    studyResult.ResultMark = resultMark;
                    studyResult.Condition = condition;
                    studyResult.ConditionReason = conditionReason;
                    if (resultMark >= passMark && condition)
                    {
                        studyResult.SubjectStatus = true;
                    } else
                    {
                        studyResult.SubjectStatus = false;
                    }
                    studyResult.UpdateUser = userUpdate;
                    studyResult.UpdateDate = DateTime.Now;
                    _context.StudyResults.Update(studyResult);
                    _context.SaveChanges();
                }
                else
                {
                    studyResult = new StudyResult();
                    studyResult.UserId = userId;
                    studyResult.ExchangeId = exchangId;
                    studyResult.ResultMark = resultMark;
                    studyResult.Condition = condition;
                    studyResult.ConditionReason = conditionReason;
                    if (resultMark >= passMark && condition)
                    {
                        studyResult.SubjectStatus = true;
                    }
                    else
                    {
                        studyResult.SubjectStatus = false;
                    }
                    studyResult.UpdateUser = userUpdate;
                    studyResult.UpdateDate = DateTime.Now;
                    _context.StudyResults.Add(studyResult);
                    _context.SaveChanges();
                }
            }
        }

        public StudyResult GetUserStudyResult(int userId, int exchangeId)
        {
            StudyResult studyResult = null;
            var result = _context.StudyResults.FirstOrDefault(p => p.UserId == userId && p.ExchangeId == exchangeId);
            if (result != null) {
                studyResult = result;
            }
            return studyResult;
        }

        public ArrayList GetAllProgramStudyExchange(int programId)
        {
            ArrayList list = new ArrayList();
            var result = _context.StudyExchanges.Where(p => p.ProgramId == programId).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public void AddNewProgramLog(int programId, string fileName, int userId, DateTime updateDate)
        {
            ProgramLog programLog = new ProgramLog();
            programLog.ProgramId = programId;
            programLog.LogPath = fileName;
            programLog.UpdateUser = userId;
            programLog.UpdateDate = updateDate;
            _context.ProgramLogs.Add(programLog);
            _context.SaveChanges();
        }

        public string GetProgramLogFileName(int program)
        {
            string fileName = "";
            ProgramLog result = _context.ProgramLogs.FirstOrDefault(p => p.ProgramId == program);
            if (result != null)
            {
                fileName = result.LogPath;
            }
            return fileName;
        }

        public void AutoChangeProgramStatus()
        {
            List<Models.Program> listProgram = _context.Programs.Where(p => p.Status != StaticsData.CREATE_STATUS).ToList();
            if (listProgram != null && listProgram.Count > 0)
            {
                foreach (Models.Program program in listProgram)
                {
                    if(program.RegisterEndDate >= DateTime.Now)
                    {
                        program.Status = StaticsData.START_STATUS;
                        _context.Programs.Update(program);
                        _context.SaveChanges();
                    }
                    if (program.RegisterEndDate < DateTime.Now && program.StartDate > DateTime.Now)
                    {
                        program.Status = StaticsData.PROCESS_STATUS;
                        _context.Programs.Update(program);
                        _context.SaveChanges();
                    }
                    if (program.StartDate <= DateTime.Now && program.EndDate >= DateTime.Now)
                    {
                        program.Status = StaticsData.HAPPEN_STATUS;
                        _context.Programs.Update(program);
                        _context.SaveChanges();
                    }
                    if (program.EndDate < DateTime.Now)
                    {
                        program.Status = StaticsData.CLOSE_STATUS;
                        _context.Programs.Update(program);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public ArrayList getAllPrograms()
        {
            AutoChangeProgramStatus();
            ArrayList listAllProgram = new ArrayList();

            var query = _context.Programs.Where(p => p.Status != StaticsData.CREATE_STATUS);

            var result = query.OrderByDescending(p => p.UpdateDate).ToList();

            if(result != null && result.Count > 0)
            {
                listAllProgram.AddRange(result);
            }

            return listAllProgram;
        }

        public Models.Program getProgramById(int Id)
        {
            Models.Program program = new Models.Program();

            var result = _context.Programs.FirstOrDefault(c => c.ProgramId == Id);
            if (result != null)
            {
                program = result;
            }

            return program;
        }

        public List<LogEntry> ReadProgramLog(int programId)
        {
            List<LogEntry> list = null;

            ProgramLog programLog = _context.ProgramLogs.FirstOrDefault(c => c.ProgramId == programId);
            if (programLog != null)
            {
                string logFileName = programLog.LogPath;
                list = StaticsData.ReadLogEntries(logFileName);
            }
            return list;
        }

        public ArrayList getListPartnerProgram(int programId)
        {
            ArrayList listAllPartnerProgram = new ArrayList();

            var query = (from pc in _context.ProgramCooperations
                         join pn in _context.Partners on pc.PartnerId equals pn.PartnerId
                         join pnc in _context.PartnerContacts on pn.PartnerId equals pnc.PartnerId
                         join ct in _context.Countries on pn.Country equals ct.IsoCode
                         where pn.IsActive == true && pc.PartnerContact == pnc.ContactId && pc.ProgramId == programId
                         select new ViewModels.ViewProgramCooperation
                         {
                             PartnerId = pn.PartnerId,
                             CooperationId = pc.CooperationId,
                             PartnerContactName = pnc.Name,
                             ContactId = pnc.ContactId,
                             ProgramId = programId,
                             PartnerName = pn.Name,
                             MaxNumberRegister = pc.MaxNumberRegister,
                             NumberOfRegister = pc.NumberOfRegister,
                             PartnerCountry = ct.Name,
                             CountryCode = ct.IsoCode
                         });

            var result = query.ToList();

            if (result != null && result.Count > 0)
            {
                listAllPartnerProgram.AddRange(result);
            }

            return listAllPartnerProgram;
        }

        public int getTotalRegisterProgram(int programID)
        {
            int number = 0;
            Register register = new Register();

            var result = _context.Registers.Where(p => p.ProgramId == programID).ToList();

            if (result != null)
            {
                number = result.Count;
            }

            return number;
        }

        public ArrayList getListProgramExchangeStudy(int programID)
        {
            ArrayList list = new ArrayList();
            var result = _context.StudyExchanges
                                 .Where(p => p.ProgramId == programID)
                                 .ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public string getListProgramSubject(int programID)
        {
            string subject = "";
            var result = _context.StudyExchanges
                                 .Where(p => p.ProgramId == programID)
                                 .ToList();

            if (result != null && result.Any())
            {
                subject = result[0].FptCourse;

                if (result.Count > 1)
                {
                    foreach (var item in result.Skip(1))
                    {
                        subject += " - " + item.FptCourse;
                    }
                }
            }

            return subject;
        }

        public bool checkUserManageProgram(int usrId, int programId)
        {
            bool check = false;
            ProgramManagement manage = _context.ProgramManagements.FirstOrDefault(x => x.ProgramId == programId && x.UserId == usrId);
            if (manage != null)
            {
                check = true;
            }
            return check;
        }

        public ArrayList getAllManagerProgram(int programId)
        {
            ArrayList list = new ArrayList();
            var result = (from pm in _context.ProgramManagements
                          join usr in _context.UsrAccounts on pm.UserId equals usr.UserId
                          where pm.ProgramId == programId
                          select new ViewModels.ViewProgramManager
                          {
                              userId = pm.UserId,
                              userName = usr.UserName,
                              userEmail = usr.Email,
                              userImg = usr.Image,
                              programId = programId,
                              ManageRole = pm.ManageRole
                          }
                          ).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public void ApproveProgram(int programId, List<string> manageProgramList, int userId, DateTime updateDate)
        {
            Models.Program program = _context.Programs.FirstOrDefault(x => x.ProgramId == programId && x.Status == StaticsData.CREATE_STATUS);
            if (program != null)
            {
                program.UserId = userId;
                program.Status = StaticsData.START_STATUS;
                program.UpdateUser = userId;
                program.UpdateDate = updateDate;
                _context.Programs.Update(program);

                if (manageProgramList.Count <= 0) return;

                for (int i = 0; i < manageProgramList.Count; i++)
                {
                    List<string> partnerElements = manageProgramList[i].ToString().Split('|').ToList();

                    ProgramManagement management = new ProgramManagement();
                    management.ProgramId = programId;
                    management.UserId = int.Parse(partnerElements[0]);
                    management.ManageRole = int.Parse(partnerElements[1]);
                    _context.ProgramManagements.Add(management);
                }

                _context.SaveChanges();
            }
        }

        public void UpdateApproveProgram(int programId, List<string> manageProgramList, int userId, DateTime updateDate)
        {
            Models.Program program = _context.Programs.FirstOrDefault(x => x.ProgramId == programId);
            if (program != null)
            {
                if (manageProgramList.Count <= 0) return;

                for (int i = 0; i < manageProgramList.Count; i++)
                {
                    List<string> partnerElements = manageProgramList[i].ToString().Split('|').ToList();

                    ProgramManagement management = _context.ProgramManagements.FirstOrDefault(p => p.ProgramId == programId && p.UserId == int.Parse(partnerElements[0]));
                    if (management != null)
                    {
                        management.ManageRole = int.Parse(partnerElements[1]);
                    }
                    else
                    {
                        management = new ProgramManagement();
                        management.ProgramId = programId;
                        management.UserId = int.Parse(partnerElements[0]);
                        management.ManageRole = int.Parse(partnerElements[1]);
                        _context.ProgramManagements.Add(management);
                    }
                }

                program.UpdateUser = userId;
                program.UpdateDate = updateDate;
                _context.Programs.Update(program);
                _context.SaveChanges();
            }
        }

        public void CloseProgram(int programId, int userId, DateTime updateDate)
        {
            Models.Program program = _context.Programs.FirstOrDefault(x => x.ProgramId == programId);
            if (program != null)
            {
                program.Status = StaticsData.CLOSE_STATUS;
                program.UpdateUser = userId;
                program.UpdateDate = updateDate;
                _context.Programs.Update(program);
                _context.SaveChanges();
            }
        }

        public ArrayList GetListProgramParticipatingForum(int userId)
        {
            ArrayList listProgramForum = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(acc => acc.UserId == userId);
            if (usrAccount != null)
            {
                if (usrAccount.Role.Equals(StaticsData.DEFAULT_ROLE))
                {
                    var result = (from p in _context.Programs
                                  join re in _context.Registers on p.ProgramId equals re.ProgramId
                                  join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                                  where usr.UserId == userId && re.RegisterStatus == StaticsData.REGISTER_ACCEPT
                                  && (p.Status == StaticsData.PROCESS_STATUS || p.Status == StaticsData.HAPPEN_STATUS) select p).ToList();

                    if (result != null && result.Count > 0)
                    {
                        listProgramForum.AddRange(result);
                    }
                }
                else
                {
                    var result = (from p in _context.Programs
                                  join pm in _context.ProgramManagements on p.ProgramId equals pm.ProgramId
                                  join usr in _context.UsrAccounts on pm.UserId equals usr.UserId
                                  where (usr.UserId == userId || p.UserId == userId)
                                  && (p.Status == StaticsData.PROCESS_STATUS || p.Status == StaticsData.HAPPEN_STATUS)
                                  select p).ToList();

                    if (result != null && result.Count > 0)
                    {
                        listProgramForum.AddRange(result);
                    }
                }
            }

            return listProgramForum;
        }

        public ArrayList GetListProgramParticipationForum(int userId)
        {
            ArrayList listProgramForum = new ArrayList();

            UsrAccount usrAccount = _context.UsrAccounts.FirstOrDefault(acc => acc.UserId == userId);
            if (usrAccount != null)
            {
                if (usrAccount.Role.Equals(StaticsData.DEFAULT_ROLE))
                {
                    var result = (from p in _context.Programs
                                  join re in _context.Registers on p.ProgramId equals re.ProgramId
                                  join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                                  where usr.UserId == userId && re.RegisterStatus == StaticsData.REGISTER_ACCEPT
                                  && p.Status == StaticsData.CLOSE_STATUS
                                  select p).ToList();

                    if (result != null && result.Count > 0)
                    {
                        listProgramForum.AddRange(result);
                    }
                }
                else
                {
                    var result = (from p in _context.Programs
                                  join pm in _context.ProgramManagements on p.ProgramId equals pm.ProgramId
                                  join usr in _context.UsrAccounts on pm.UserId equals usr.UserId
                                  where (usr.UserId == userId || p.UserId == userId)
                                  && p.Status == StaticsData.CLOSE_STATUS
                                  select p).ToList();

                    if (result != null && result.Count > 0)
                    {
                        listProgramForum.AddRange(result);
                    }
                }
            }

            return listProgramForum;
        }

        public ViewCountProgram CountProgramList()
        {
            ViewCountProgram view = new ViewCountProgram();

            view.NumberProgram = _context.Programs.Where(p => p.Status != StaticsData.CREATE_STATUS).Count();
            view.StartProgram = _context.Programs.Where(p => p.Status != StaticsData.CLOSE_STATUS && p.Status != StaticsData.CREATE_STATUS).Count();
            view.CloseProgram = _context.Programs.Where(p => p.Status == StaticsData.CLOSE_STATUS).Count();

            return view;
        }

        public ArrayList GetDashBoardProgram()
        {
            ArrayList list = new ArrayList();
            SystemsService systemsService = new SystemsService(_context);
            CountryService countryService = new CountryService(_context);
            RegisterService registerService = new RegisterService(_context);
            var result = _context.Programs.Where(p => p.Status != StaticsData.CREATE_STATUS).ToList();

            if (result != null && result.Count > 0)
            {
                int count = 1;
                foreach (Models.Program program in result)
                {
                    ViewDashBoard dashBoard = new ViewDashBoard();
                    dashBoard.count = count;
                    dashBoard.programId = program.ProgramId;
                    dashBoard.programCategory = systemsService.GetMasterNameByType(program.Categorize.Trim());
                    dashBoard.programName = program.Title;
                    dashBoard.startDate = program.StartDate;
                    dashBoard.endDate = program.EndDate;
                    dashBoard.status = program.Status;

                    ArrayList listPartner = getListPartnerProgram(program.ProgramId);
                    if (listPartner != null && listPartner.Count > 0)
                    {
                        List<string> PartnerList = new List<string>();
                        List<string> CountryList = new List<string>();

                        foreach (ViewProgramCooperation view in listPartner)
                        {
                            if (!PartnerList.Contains(view.PartnerName)) {
                                PartnerList.Add(view.PartnerName);
                            }
                            if (!CountryList.Contains(view.PartnerCountry))
                            {
                                CountryList.Add(view.PartnerCountry);
                            }
                        }
                        dashBoard.partner = PartnerList.ToArray();
                        dashBoard.country = CountryList.ToArray();
                    }
                    else
                    {
                        dashBoard.partner = new string[0];
                        dashBoard.country = new string[] { countryService.GetCountryName(program.Country) };
                    }

                    ArrayList listRegister = registerService.GetAllProgramRegister(program.ProgramId);
                    if (listPartner == null || listPartner.Count <= 0)
                    {
                        listRegister = registerService.GetAllProgramNonePartnerRegister(program.ProgramId);
                    }
                    if (listRegister != null && listRegister.Count > 0)
                    {
                        dashBoard.numberRegister = listRegister.Count;
                        int countRegisted = 0;
                        foreach (ViewRegisterProgram register in listRegister)
                        {
                            if (register.RegisterStatus == StaticsData.REGISTER_ACCEPT)
                            {
                                countRegisted++;
                            }
                        }
                        dashBoard.numberAccept = countRegisted;
                    }

                    list.Add(dashBoard);
                }
            }

            return list;
        }
    }
}
