using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZURNA.BL.BusinessModel;
using ZURNA.DAL.Table;

namespace ZURNA.BL.BusinessLayer
{
    /// <summary>
    /// Strategy Desing Pattern
    /// </summary>
    public class HashTagManager
    {
        private IHashTagBL _hash;
        public HashTagManager(IHashTagBL tagmanager)
        {
            _hash = tagmanager;
        }

    }
    public interface IHashTagBL
    {
        Task<List<Post>> GetPostByTagNames(string hashtag);
        Task<List<Post>> GetCityPost(string city);
        HashtagModel GetExtractPost(string posttext);
        Task<List<string>> GetPopularTags(DateTime datetime);     
    }
    public class HashTagsBL : IHashTagBL
    {
        private BusinessPost _post;
        public HashTagsBL()
        {
            _post = new BusinessPost();
        }
        public async Task<List<Post>> GetCityPost(string city)
        {
            var result = await _post.GetList();
            result = result.Where(sa => sa.content.location != null
              &&
              sa.content.location.city.Contains(city))
            .ToList();
            return result;
        }

        public HashtagModel GetExtractPost(string posttext)
        {
            /*
             Örnek : Bugün ##Misaş ve #Bi#m'de Mükemmel İndirimler var...
             */
            HashtagModel hashtagmodel = new HashtagModel();
            bool ishashtag = false;
            string bulunantag = String.Empty;
            List<string> hashtag = new List<string>();
            string orginal = posttext;
            List<string> postlist = posttext.Split(' ').ToList();
            foreach (var item in postlist)
            {
                if (ishashtag == false)
                {
                    if (item.StartsWith("#"))
                    {
                        int count = item.Count(sa => sa.ToString() == "#");
                        char[] resultTag = new char[item.Length];
                        bulunantag = item;
                        var filter = item;
                        if (count > 1)
                        {
                            for (int i = 1; i < bulunantag.Length; i++)
                            {
                                if (bulunantag[i].ToString() != "#")
                                {
                                    resultTag[i] = bulunantag[i];
                                }
                                else
                                {
                                    resultTag[i] = ' ';
                                }
                            }
                            resultTag[0] = '#';
                            bulunantag = new string(resultTag);
                            bulunantag = bulunantag.Replace(" ", "");
                            posttext = posttext.Replace(item, bulunantag);
                        }

                        ishashtag = true;
                    }
                }
                else if (ishashtag == true)
                {
                    var filter = item;
                    filter = filter.Replace("#", "");
                    posttext = posttext.Replace(item, filter);
                }
            }
            hashtagmodel.HashTag = bulunantag;
            hashtagmodel.OnlyText = posttext;
            return hashtagmodel;

        }       
        public async Task<List<string>> GetPopularTags()
        {
            var popularpost = await _post.GetPopularPost();
            List<string> tags= popularpost.
                Where(sa => sa.content.hashtag != null)
                .Select(d => d.content.hashtag).ToList();
            return tags;
        }

        public async Task<List<string>> GetPopularTags(DateTime datetime)
        {
            var popularpost = await _post.GetPopularPost();
            popularpost = popularpost.OrderByDescending(sa => sa.content.time.AddDays(-1) <= datetime).ToList();
            List<string> tags = popularpost.
                Where(sa => sa.content.hashtag != null)
                .Select(d => d.content.hashtag).ToList();
            return tags;
        }
        public async Task<List<Post>> GetPostByTagNames(string hashtag)
        {
            var result = await _post.GetList();
            result = result.Where(sa => sa.content != null && sa.content.hashtag != null
              && sa.content.hashtag.Contains(hashtag)).ToList();
            return result;
        }

    }
}
