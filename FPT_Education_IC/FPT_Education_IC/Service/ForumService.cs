using FPT_Education_IC.Data;
using FPT_Education_IC.Models;
using FPT_Education_IC.ViewModels;
using System.Collections;

namespace FPT_Education_IC.Service
{
    public class ForumService
    {
        private readonly DataContext _context;

        public ForumService(DataContext context)
        {
            _context = context;
        }

        public int AddNewForum(int programId, string title, string content, string imgPath, int userUpdate)
        {
            ProgramPost programPost = new ProgramPost();
            programPost.ProgramId = programId;
            programPost.Title = title;
            programPost.PostContent = content;
            programPost.Image = imgPath;
            programPost.UpdateUser = userUpdate;
            programPost.UpdateDate = DateTime.Now;

            _context.ProgramPosts.Add(programPost);
            _context.SaveChanges();

            return programPost.PotsId;
        }

        public void AddNewPostComment(int userId, int postId, string comment)
        {
            PostComment postComment = new PostComment();
            postComment.PostId = postId;
            postComment.UpdateUser = userId;
            postComment.CommentText = comment;
            postComment.UpdateDate = DateTime.Now;

            _context.PostComments.Add(postComment);
            _context.SaveChanges();
        }

        public ArrayList GetAllCommentPost(int postId)
        {
            ArrayList list = new ArrayList();
            var result = _context.PostComments.Where(p => p.PostId == postId).OrderByDescending(p => p.UpdateDate).ToList();
            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public ArrayList GetAllProgramPost(int programId)
        {
            ArrayList list = new ArrayList();
            var result = _context.ProgramPosts.Where(p => p.ProgramId == programId).OrderByDescending(p => p.UpdateDate).ToList();
            if(result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }

        public ArrayList GetAllUserInProgram(int programId)
        {
            ArrayList list = new ArrayList();
            var query = from ua in _context.UsrAccounts
                        where (from pm in _context.ProgramManagements
                               where pm.ProgramId == programId
                               select pm.UserId)
                              .Union(from re in _context.Registers
                                     where re.ProgramId == programId
                                     select re.UserId)
                              .Contains(ua.UserId)
                        select ua;

            var result = query.ToList();

            if (result != null && result.Count > 0)
            {
                list.AddRange(result);
            }
            return list;
        }
    }
}
