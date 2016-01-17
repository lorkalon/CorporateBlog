using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class CommentRateService:BaseService, ICommentRateService
    {
        private readonly ICommentRateRepository _commentRateRepository;
        public CommentRateService(
            IContextProvider contextProvider, 
            ICommentRateRepository commentRateRepository) : base(contextProvider)
        {
            _commentRateRepository = commentRateRepository;
        }
        public async Task AddRate(Common.CommentRate rate)
        {
            var entity = Mapper.Map<DAL.Models.CommentRate>(rate);
            _commentRateRepository.Add(entity);
            await SaveChangesAsync();
        }

        public Common.CommentRate FindCommentRate(int commentId, int userId)
        {
            var entity =
                _commentRateRepository.GetFiltered(rate => rate.CommentId == commentId && rate.UserId == userId)
                    .FirstOrDefault();
            return Mapper.Map<Common.CommentRate>(entity);
        }
    }
}
