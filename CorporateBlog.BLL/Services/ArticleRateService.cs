using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class ArticleRateService:BaseService, IArticleRateService
    {
        private readonly IArticleRateRepository _articleRateRepository;
        public ArticleRateService(IContextProvider contextProvider, IArticleRateRepository articleRateRepository) : base(contextProvider)
        {
            _articleRateRepository = articleRateRepository;
        }

        public async Task AddRate(Common.ArticleRate articleRate)
        {
            var isVoted =
                _articleRateRepository.GetFiltered(
                    rate => rate.UserId == articleRate.UserId && 
                            rate.ArticleId == articleRate.ArticleId).FirstOrDefault();

            if (isVoted == null)
            {
                var model = Mapper.Map<DAL.Models.ArticleRate>(articleRate);
                _articleRateRepository.Add(model);
                await SaveChangesAsync();
            }
        }

        public Common.ArticleRate GetRateByFilter(ArticleRateFilter filter)
        {
            var userRate = _articleRateRepository.GetFiltered(
                rate => rate.UserId == filter.UserId && rate.ArticleId == filter.ArticleId).FirstOrDefault();

            if (userRate != null)
            {
                return Mapper.Map<Common.ArticleRate>(userRate);
            }

            return null;
        }
    }
}
