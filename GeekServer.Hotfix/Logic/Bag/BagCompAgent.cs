﻿using Geek.Server.Proto;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventNext;
using System;

namespace Geek.Server.Logic.Bag
{

    [Service(typeof(ISerializable), Name = "fff", SingleInstance = true)]
    public class BagCompAgent : StateComponentAgent<BagComp, BagState>
    {
        public Task Init()
        {
            //赠送默认道具
            if (State.ItemMap.Count <= 0)
            {
                State.ItemMap.Add(101, 1);
                State.ItemMap.Add(103, 100);
            }
            return Task.CompletedTask;
        }

        public Task<long> GetItemNum(int itemId)
        {
            long num = 0;
            if (State.ItemMap.ContainsKey(itemId))
                num = State.ItemMap[itemId];
            return Task.FromResult(num);
        }

        public Task AddItem(int itemId, long num)
        {
            if (State.ItemMap.ContainsKey(itemId))
                State.ItemMap[itemId] += num;
            else
                State.ItemMap[itemId] = num;
            return Task.CompletedTask;
        }

        public Task CutItem(int itemId, long num)
        {
            if (State.ItemMap.ContainsKey(itemId))
            {
                State.ItemMap[itemId] -= num;
                if (State.ItemMap[itemId] <= 0)
                    State.ItemMap.Remove(itemId);
            }
            return Task.CompletedTask;
        }

        public Task<ResBagInfo> BuildInfoMsg()
        {
            var res = new ResBagInfo();
            foreach (var kv in State.ItemMap)
                res.ItemDic[kv.Key] = kv.Value;
            return Task.FromResult(res);
        }

        public Task Test1()
        {
            return Task.CompletedTask;
        }

        [MethodOption.ThreadSafe]
        public virtual Task Test2()
        {
            System.Console.WriteLine("Test2 be Called");
            Test4();
            Test3(1, 2, 3, 4, "");
            return Task.CompletedTask;
        }

        public Task Test3(int a, int b, int c, int d, string e)
        {
            return Task.CompletedTask;
        }


        [MethodOption.NotAwait]
        public virtual Task Test4()
        {
            System.Console.WriteLine("Test412 be Called");
            return Task.CompletedTask;
        }


        public virtual Task Test5(int a, List<int> list)
        {
            if (IsRemoting)
            {
                ISerializable ser = null;
                ser.Deserialize(null);
            }
            return Task.CompletedTask;
        }

        [MethodOption.NotAwait]
        public virtual Task Test6(int a, List<int> list)
        {
            if (IsRemoting)
            {
                Type[] argsType = new Type[2];
                argsType[0] = typeof(int);
                argsType[2] = typeof(List<int>);
                return CallRemote("Test6", 3, argsType, a, list);
            }
            return Task.CompletedTask;
        }


        //public virtual async Task<T> Test7<T>(int b, T a, List<int> list)
        //{
        //    await Task.Delay(1);
        //    return default;
        //}


        [MethodOption.NotAwait]
        public virtual Task Test8()
        {
            return Task.CompletedTask;
        }

        [MethodOption.ThreadSafe]
        [MethodOption.NotAwait]
        public virtual Task Test9(int a, List<int> list)
        {
            if (IsRemoting)
            {
                Type[] argsType = new Type[2];
                argsType[0] = typeof(int);
                argsType[2] = typeof(List<int>);
                return CallRemote("Test6", 3, argsType, a, list);
            }
            return Task.CompletedTask;
        }


        public virtual Task<int> Test10(int a, List<int> list)
        {
            if (IsRemoting)
            {
                Type[] argsType = new Type[2];
                argsType[0] = typeof(int);
                argsType[2] = typeof(List<int>);
                return CallRemote<int>("Test6", 3, argsType, a, list);
            }
            return Task.FromResult(10);
        }

    }
}
