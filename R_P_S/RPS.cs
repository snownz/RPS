using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace R_P_S
{
    public class RPS
    {
        private delegate void AsyncMethodCaller(Group node, ref Key[] keys, int index);

        private bool _validateTuple(string str)
        {
            var pattern = @"\[[\n\s]{0,}\""[\n\s]{0,}([\sa-zA-Z0-9\.]{1,})\""[\s\,]{1,}\""([rRpPsS]{1})\""[\s]{0,}\]";
            var reg = new Regex(pattern);
            return reg.IsMatch(str);
        }
        private StrategyModel _compare(StrategyModel st1, StrategyModel st2)
        {
            if (st1 > st2)
            {
                return st1;
            }

            if (st1 < st2)
            {
                return st2;
            }

            return st1;
        }
        private StrategyModel[] toSinglePlay(string data)
        {
            JArray list;
            try
            {
                list = JArray.Parse(data);                
            }
            catch (Exception)
            {
                throw new InvalidInputError();
            }

            if (list.Any(x => !_validateTuple(x.Value<object>().ToString())))
            {
                throw new InvalidInputError();
            }

            var _l = list
                .Select(x => new StrategyModel
                {
                    Player = x[0].Value<string>(),
                    Choice = (Played)Enum.Parse(typeof(Played), x[1].Value<string>())
                }).ToArray();

            if (!_verifyLength(_l))
            {
                throw new NoSuchStrategyError();
            }
            else
            {
                return _l;
            }
        }
        private Group[] toMultiplePlay(string data)
        {
            var grupos = new List<Group>();
            foreach (var item in JArray.Parse(data).ToList())
            {
                var g = new Group();
                var keys = new List<Key>();
                foreach (var _item in item.ToList())
                {
                    var st = toSinglePlay(_item.ToString());
                    keys.Add(new Key { Player0 = st[0], Player1 = st[1]});
                }
                g.Keys = keys.ToArray();
                grupos.Add(g);
            }
            return grupos.ToArray();
        }       
        private bool _verifyLength(StrategyModel[]  model)
        {
            return model.Length == 2;
        }
        private Key _rps_tournament_winner(Group node)
        {
            int kLenght = 0;
            var k = node.Keys;
            do
            {
                foreach (var item in k)
                {
                    if(item.Player1 != null)
                    {
                        item.Winner = _compare(item.Player0, item.Player1);
                    }
                    else
                    {
                        item.Winner = item.Player0;
                    }
                }

                kLenght = (k.Count() / 2) + (k.Count() % 2);
                var aux = new Key[kLenght];

                for (int i = 0; i < kLenght - ((k.Count() % 2)); i++)
                {
                    aux[i] = new Key
                    {
                        Player0 = k[i * 2].Winner,
                        Player1 = k[(i * 2) + 1].Winner
                    };
                }

                if(aux[kLenght - 1] == null)
                {
                    aux[kLenght - 1] = new Key
                    {
                        Player0 = k[k.Count() - 1].Winner
                    };                
                }

                k = aux;
            } while (kLenght > 1);

            return k[0];
        }
        private void _async_rps_tournament_winner(Group node, ref Key[] keys, int index)
        {
            int kLenght = 0;
            var k = node.Keys;
            do
            {
                foreach (var item in k)
                {
                    if (item.Player1 != null)
                    {
                        item.Winner = _compare(item.Player0, item.Player1);
                    }
                    else
                    {
                        item.Winner = item.Player0;
                    }
                }

                kLenght = (k.Count() / 2) + (k.Count() % 2);
                var aux = new Key[kLenght];

                for (int i = 0; i < kLenght - ((k.Count() % 2)); i++)
                {
                    aux[i] = new Key
                    {
                        Player0 = k[i * 2].Winner,
                        Player1 = k[(i * 2) + 1].Winner
                    };
                }

                if (aux[kLenght - 1] == null)
                {
                    aux[kLenght - 1] = new Key
                    {
                        Player0 = k[k.Count() - 1].Winner
                    };
                }

                k = aux;
            } while (kLenght > 1);

            keys[index] = k[0];
        }

        public string rps_game_winner(string data)
        {
            var model = toSinglePlay(data);           
            return _compare(model[0], model[1]).Player;
        }
        public string rps_tournament_winner(string data)
        {
            AsyncMethodCaller caller = new AsyncMethodCaller(_async_rps_tournament_winner);

            var model = toMultiplePlay(data);
            var keys = new Key[model.Count()];
            var thread = new object[model.Count()];
  
            for (int i = 0; i < model.Count(); i++)
            {
                thread[i] = caller.BeginInvoke(model[i], ref keys, i, null, null);
            }
            thread.ToList().ForEach(x => (x as IAsyncResult).AsyncWaitHandle.WaitOne());

            var g = new Group()
            {
                Keys = keys
            };
            var key = _rps_tournament_winner(g);
            return _compare(key.Player0, key.Player1).Player; 
        }
    }
}
