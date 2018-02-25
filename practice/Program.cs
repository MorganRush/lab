using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace practice
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

    namespace Beise
    {
        [DataContract]
        public class SaveInfo
        {
            [DataMember]
            public IDictionary<String, Int32> Spam { get; set; }
            [DataMember]
            public IDictionary<String, Int32> NotSpam { get; set; }
            [DataMember]
            public Int32 SpamCount { get; set; }
            [DataMember]
            public Int32 NotSpamCount { get; set; }
        }
        public class Core
        {
            private Int32 _spamDocCount;
            private Int32 _nSpamDocCount;
            readonly IDictionary<String, Int32> _spam = new Dictionary<String, Int32>();
            readonly IDictionary<String, Int32> _nSpam = new Dictionary<String, Int32>();

            public Core() { }

            public Core(SaveInfo saveInfo)
            {
                _spam = saveInfo.Spam;
                _nSpam = saveInfo.NotSpam;
                _nSpamDocCount = saveInfo.NotSpamCount;
                _spamDocCount = saveInfo.SpamCount;
            }

            public Core(IEnumerable<String> spamDoc, IEnumerable<String> nspamDoc)
            {
                foreach (var s in spamDoc)
                {
                    InitSpam(s);
                }
                foreach (var s in nspamDoc)
                {
                    InitNSpam(s);
                }
            }
            /// <summary>
            /// Заполняем базу знаний о спаме
            /// </summary>
            /// <param name="s"></param>
            public void InitSpam(String s)
            {
                var strings = s.ToLower().Split('\r', '\n', '\t', ' ', ',', '.', ':', ';', ')', '(');
                foreach (var str in strings.Where(x => !String.IsNullOrWhiteSpace(x)))
                {
                    if (_spam.ContainsKey(str))
                        _spam[str]++;
                    else
                    {
                        _spam.Add(str, 1);
                    }
                }
                _spamDocCount++;
            }
            /// <summary>
            /// заполняем базу о не спаме
            /// </summary>
            /// <param name="s"></param>
            public void InitNSpam(String s)
            {
                var strings = s.ToLower().Split('\r', '\n', '\t', ' ', ',', '.', ':', ';', ')', '(');
                foreach (var str in strings.Where(x => !String.IsNullOrWhiteSpace(x)))
                {
                    if (_nSpam.ContainsKey(str))
                        _nSpam[str]++;
                    else
                    {
                        _nSpam.Add(str, 1);
                    }
                }
                _nSpamDocCount++;
            }
            /// <summary>
            /// получить оценку на вероятность пренадлежности сообщений к группе спам
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public Double TestToSpam(String s)
            {
                var rate = Math.Log(_spamDocCount / (float)(_spamDocCount + _nSpamDocCount));
                var strings = s.ToLower().Split('\r', '\n', '\t', ' ', ',', '.', ':', ';', ')', '(');
                foreach (var str in strings.Where(x => !String.IsNullOrWhiteSpace(x)))
                {
                    float count = 1;
                    float lc = _spam.Count;//_spam.Sum(x => x.Value);//не втупил возможно достаточно просто _spam.Count
                    float v = (_spam.Count + _nSpam.Count);
                    if (_spam.ContainsKey(str))
                        count += _spam[str];
                    rate += Math.Log(count / (v + lc));
                }
                return rate;
            }
            /// <summary>
            /// получить оценку на вероятность пренадлежности сообщений к группе НЕ спам
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public Double TestToNotSpam(String s)
            {
                var rate = Math.Log(_nSpamDocCount / (float)(_spamDocCount + _nSpamDocCount));
                var strings = s.ToLower().Split('\r', '\n', '\t', ' ', ',', '.', ':', ';', ')', '(');
                foreach (var str in strings.Where(x => !String.IsNullOrWhiteSpace(x)))
                {
                    float count = 1;
                    float lc = _nSpam.Count;//_nSpam.Sum(x => x.Value);//не втупил возможно достаточно просто _spam.Count
                    float v = (_spam.Count + _nSpam.Count);
                    if (_nSpam.ContainsKey(str))
                        count += _nSpam[str];
                    rate += Math.Log(count / (v + lc));
                }
                return rate;
            }

            public String Test(String s)
            {
                var nSpam = TestToNotSpam(s);
                var spam = TestToSpam(s);
                var spamProc = Math.Exp(spam) / (Math.Exp(spam) + Math.Exp(nSpam));
                var nspamProc = Math.Exp(nSpam) / (Math.Exp(spam) + Math.Exp(nSpam));
                return String.Format("Вероятность что это спам = {0}%;\r\nВероятность что это не спам = {1}%;", spamProc, nspamProc);
            }
            public SaveInfo GetSaveObj()
            {
                return new SaveInfo
                {
                    Spam = _spam,
                    NotSpam = _nSpam,
                    NotSpamCount = _nSpamDocCount,
                    SpamCount = _spamDocCount
                };
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {

        }
    }
}
