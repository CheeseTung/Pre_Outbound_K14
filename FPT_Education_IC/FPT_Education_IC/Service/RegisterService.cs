using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.Statics;
using FPT_Education_IC.ViewModels;
using System;
using System.Collections;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FPT_Education_IC.Service
{
    public class RegisterService
    {
        private readonly DataContext _context;

        public RegisterService(DataContext context)
        {
            _context = context;
        }

        public int addRegisterService(int programId, int usrId, string partnerId)
        {
            int regisId = -1;
            Register register = new Register();
            register.ProgramId = programId;
            register.UserId = usrId;
            register.RegisterDate = DateTime.Now;
            register.RegisterStatus = StaticsData.REGISTER_REQUEST;
            register.RequestChange = null;
            register.RequestCancel = null;
            register.UpdateUser = usrId;
            register.UpdateDate = DateTime.Now;
            if (!string.IsNullOrEmpty(partnerId))
            {
                register.PartnerId = int.Parse(partnerId);
            }
            _context.Registers.Add(register);
            _context.SaveChanges();

            regisId = register.RegisterId;
            return regisId;

        }

        public ArrayList GetAllProgramRegister(int programId)
        {
            ArrayList list = new ArrayList();
            var query = (from re in _context.Registers
                          join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                          join pn in _context.Partners on re.PartnerId equals pn.PartnerId
                          join ct in _context.Countries on pn.Country equals ct.IsoCode
                          where re.ProgramId == programId
                          select new ViewModels.ViewRegisterProgram
                          {
                              UserId = usr.UserId,
                              RegisterId = re.RegisterId,
                              UserName = usr.UserName,
                              UserEmail = usr.Email,
                              UserRollNumber = usr.RollNumber,
                              UpdateDate = re.UpdateDate,
                              PartnerName = pn.Name,
                              PartnerCountry = ct.Name,
                              RegisterStatus = re.RegisterStatus,
                              RequestChange = re.RequestChange,
                              RequestCancel = re.RequestCancel
                          }
                          );

            var result = query.ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public ArrayList GetAllProgramNonePartnerRegister(int programId)
        {
            ArrayList list = new ArrayList();
            var query = (from re in _context.Registers
                         join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                         where re.ProgramId == programId
                         select new ViewModels.ViewRegisterProgram
                         {
                             UserId = usr.UserId,
                             RegisterId = re.RegisterId,
                             UserName = usr.UserName,
                             UserEmail = usr.Email,
                             UserRollNumber = usr.RollNumber,
                             UpdateDate = re.UpdateDate,
                             RegisterStatus = re.RegisterStatus,
                             RequestChange = re.RequestChange,
                             RequestCancel = re.RequestCancel
                         }
                          );

            var result = query.ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public PaymentLog getRegisterPayment(int registerId, int programId)
        {
            PaymentLog paymentLog = null;
            var result = _context.PaymentLogs.FirstOrDefault(p => p.RegisterId == registerId && p.ProgramId == programId);
            if (result != null)
            {
                paymentLog = result;
            }

            return paymentLog;
        }

        public ArrayList getUserPaymentLogs(int usrId) {
            ArrayList list = new ArrayList();
            var result = (from log in _context.PaymentLogs
                          join re in _context.Registers on log.RegisterId equals re.RegisterId
                          join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                          join pr in _context.Programs on log.ProgramId equals pr.ProgramId
                          where re.UserId == usrId && usr.UserId == usrId && re.ProgramId == pr.ProgramId
                          select new UsrPaymentLog
                          {
                              logId = log.LogId,
                              programName = pr.Title,
                              paymentValue = log.PaymentValue,
                              paymentDate = log.PaymentDate,
                              paymentEndDate = pr.PaymentEndDate,
                              status = log.Status
                          }
                          ).OrderByDescending(p => p.paymentDate).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }


        public ArrayList getUserMarkLogs(int usrId)
        {
            ArrayList list = new ArrayList();
            var result = (from re in _context.StudyResults
                          join st in _context.StudyExchanges on re.ExchangeId equals st.ExchangeId
                          join pr in _context.Programs on st.ProgramId equals pr.ProgramId
                          join usr in _context.UsrAccounts on re.UserId equals usr.UserId
                          where re.UserId == usrId && usr.UserId == usrId && (pr.Status == StaticsData.CLOSE_STATUS || pr.Status == StaticsData.HAPPEN_STATUS)
                          select new UsrStudyLog
                          {
                             ProgramName = pr.Title,
                             CourseName = st.CourseName,
                             FPTCourse = st.FptCourse,
                             ResultMark = re.ResultMark,
                             MaxMark = st.MaxMark,
                             PassMark = st.PassMark,
                             Note = re.ConditionReason,
                             Status = re.SubjectStatus,
                             ProgramEndDate = pr.EndDate
                          }
                          ).OrderByDescending(p => p.ProgramEndDate).ToList();


            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public void UpdateUsrRegister(int registerId, int usrId, string partnerId)
        {
            Register register = getRegisterById(registerId);
            register.UpdateUser = usrId;
            register.UpdateDate = DateTime.Now;
            if (!string.IsNullOrEmpty(partnerId))
            {
                register.PartnerId = int.Parse(partnerId);
            }
            _context.Registers.Update(register);
            _context.SaveChanges();
        }

        public Register getUserRegister(int programId, int usrId)
        {
            Register register = null;
            var result = _context.Registers.FirstOrDefault(p => p.ProgramId == programId && p.UserId == usrId);
            if (result != null)
            {
                register = result;
            }

            return register;
        }

        public Register getRegisterById(int registerId)
        {
            Register register = null;
            var result = _context.Registers.FirstOrDefault(p => p.RegisterId == registerId);
            if (result != null)
            {
                register = result;
            }

            return register;
        }

        public void AddAnswerRegister(int usrId, string[] questionID, string[] answerQuestion)
        {
            if (answerQuestion != null && answerQuestion.Length > 0 && questionID != null && questionID.Length > 0)
            {
                for(int i =0; i < questionID.Length; i++)
                {
                    RegisterAnswer registerAnswer = new RegisterAnswer();
                    registerAnswer.QuestionId = int.Parse(questionID[i]);
                    registerAnswer.AnswerContent = answerQuestion[i];
                    registerAnswer.RegisterId = usrId;
                    _context.RegisterAnswers.Add(registerAnswer);
                    _context.SaveChanges();
                }
            }
        }

        public void UpdateAnswerRegister(int usrId, string[] questionID, string[] answerQuestion)
        {
            if (answerQuestion != null && answerQuestion.Length > 0 && questionID != null && questionID.Length > 0)
            {
                for (int i = 0; i < questionID.Length; i++)
                {
                    RegisterAnswer registerAnswer = _context.RegisterAnswers.FirstOrDefault(x => x.QuestionId == int.Parse(questionID[i]));
                    if (registerAnswer != null)
                    {
                        registerAnswer.AnswerContent = answerQuestion[i];
                        registerAnswer.RegisterId = usrId;
                        _context.RegisterAnswers.Update(registerAnswer);
                        _context.SaveChanges();
                    }
                }
            }
        }

        public void UpdateRegisterStatus(int registerId, int usrId, int status, string RequestChange, string RequestCancel)
        {
            Register register = getRegisterById(registerId);
            register.RegisterStatus = status;
            if (!string.IsNullOrEmpty(RequestChange))
            {
                register.RequestChange = RequestChange;
            }
            if (!string.IsNullOrEmpty(RequestCancel))
            {
                register.RequestCancel = RequestCancel;
            }
            register.UpdateUser = usrId;
            register.UpdateDate = DateTime.Now;
            _context.Registers.Update(register);
            _context.SaveChanges();
        }

        public void UpdateRegisterPayment(int programId, int registerId, int usrId, int status, Decimal paymentValue, DateTime paymentDate)
        {
            var result = _context.PaymentLogs.FirstOrDefault(p => p.RegisterId == registerId && p.ProgramId == programId);

            if (result == null)
            {
                PaymentLog paymentLog = new PaymentLog();
                paymentLog.ProgramId = programId;
                paymentLog.RegisterId = registerId;
                paymentLog.Status = status;
                paymentLog.PaymentValue = paymentValue;
                paymentLog.PaymentDate = paymentDate;
                paymentLog.UpdateUser = usrId;
                paymentLog.UpdateDate = DateTime.Now;
                _context.PaymentLogs.Add(paymentLog);
                _context.SaveChanges();
            }
            else
            {
                PaymentLog paymentLog = result;
                paymentLog.PaymentValue += paymentValue;
                paymentLog.PaymentDate = paymentDate;
                paymentLog.Status = status;
                paymentLog.UpdateUser = usrId;
                paymentLog.UpdateDate = DateTime.Now;
                _context.PaymentLogs.Update(paymentLog);
                _context.SaveChanges();
            }
        }


        public ArrayList GetUserListRegisterProgram(int userId)
        {
            ArrayList list = new ArrayList();

            var result = (from p in _context.Programs
                            join re in _context.Registers on p.ProgramId equals re.ProgramId
                            where re.UserId == userId && (re.RegisterStatus == StaticsData.REGISTER_REQUEST || re.RegisterStatus == StaticsData.REGISTER_CANCEL)
                          select new ViewUserReigisterProgram
                          {
                              Programs = p,
                              Registers = re
                          }
                          ).ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public ArrayList GetUserListParticipationProgram(int userId)
        {
            ArrayList list = new ArrayList();

            var result = (from p in _context.Programs
                          join re in _context.Registers on p.ProgramId equals re.ProgramId
                          where re.UserId == userId && p.Status == StaticsData.CLOSE_STATUS && re.RegisterStatus == StaticsData.REGISTER_ACCEPT
                          select new ViewUserReigisterProgram
                          {
                              Programs = p,
                              Registers = re
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public ArrayList GetUserListParticipatingProgram(int userId)
        {
            ArrayList list = new ArrayList();

            var result = (from p in _context.Programs
                          join re in _context.Registers on p.ProgramId equals re.ProgramId
                          where re.UserId == userId && (p.Status == StaticsData.START_STATUS || p.Status == StaticsData.PROCESS_STATUS || p.Status == StaticsData.HAPPEN_STATUS)
                          && (re.RegisterStatus == StaticsData.REGISTER_PENDING || re.RegisterStatus == StaticsData.REGISTER_ACCEPT)
                          select new ViewUserReigisterProgram
                          {
                              Programs = p,
                              Registers = re
                          }).ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }

            return list;
        }

        public void AddProgramFeedBack(int programId, int UserId, int programContent, int facilities, int partnerSupport,
            int extracurricular, int staffSupport, string? feedbackContent, DateTime updateDate)
        {
            Feedback feedback = new Feedback();
            feedback.ProgramId = programId;
            feedback.UserId = UserId;
            feedback.ProgramContent = programContent;
            feedback.Facilities = facilities;
            feedback.PartnerSupport = partnerSupport;
            feedback.Extracurricular = extracurricular;
            feedback.StaffSupport = staffSupport;
            feedback.Feedback1 = feedbackContent;
            feedback.UpdateDate = updateDate;

            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public ArrayList GetAllProgramFeedBack(int programId)
        {
            ArrayList list = new ArrayList();
            var result = _context.Feedbacks.Where(f => f.ProgramId == programId).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public bool CheckUserFeedback(int programID, int userID)
        {
            var result = _context.Feedbacks.FirstOrDefault(p => p.ProgramId == programID && p.UserId == userID);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
