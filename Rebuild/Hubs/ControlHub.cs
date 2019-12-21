using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rebuild.Hubs
{
    public class ControlHub : Hub
    {
        private static string[] Users { set; get; } = new string[4];
        private static string[] Directions { set; get; } = new string[4];
        private static bool[] Ready { set; get; } = new bool[4];
        private static bool[] Alive { set; get; } = new bool[4] { false,false,false,false};
        private static int[] HeadIndex = new int[4] {2,119,1597,1480};
        private static int[] TailIndex = new int[4] {0,39,1599,1560};
        private static String[] Map { set; get; } = new String[1600];
        private static int counter = 0;
        private static int appleCounter = 0;
        public async Task Reset() {
            Users = new string[4];
            Directions = new string[4];
            Ready= new bool[4];
            Alive= new bool[4] { false, false, false, false };
            HeadIndex = new int[4] { 2, 119, 1597, 1480 };
            TailIndex = new int[4] { 0, 39, 1599, 1560 };
            Map= new String[1600];
            counter = 0;
            appleCounter = 0;
            await Clients.All.SendAsync("Clear");
        }
        public async Task LogIn(string user) {
            if (counter < 4)
            {
                Users[counter] = user;
                Ready[counter] = counter == 0;
                Alive[counter] = true;
                switch (counter)
                {
                    case 0:
                        Directions[counter] = "d";
                        break;
                    case 1:
                        Directions[counter] = "s";
                        break;
                    case 2:
                        Directions[counter] = "a";
                        break;
                    case 3:
                        Directions[counter] = "w";
                        break;
                }
                counter++;
                await (counter == 1 ? Clients.Caller.SendAsync("Success", "host") : Clients.All.SendAsync("Success", "player"));
            }
            else { 
                await Clients.Caller.SendAsync("Fail","none");
            }
        }
        public async Task ReadyCheck(string user)
        {
            for (int i = 0; i < Users.Length; i++) {
                if (Users[i] == user) {
                    Ready[i] = true;
                }
            }
            for (int i = 0; i < Users.Length; i++)
            {
                if (Ready[i] == false)
                {
                    await Clients.Caller.SendAsync("ReadySuccess");
                }
            }
            await Clients.All.SendAsync("ReadySuccess");
        }
        public async Task Start() {
            for (int i = 0; i < 1600; i++) {
                switch(i){
                    case 0: 
                        Map[i] = "p1rt";
                        break;
                    case 1:
                        Map[i] = "p1r";
                        break;
                    case 2:
                        Map[i] = "p1h";
                        break;
                    case 39:
                        Map[i] = "p2dt";
                        break;
                    case 79:
                        Map[i] = "p2d";
                        break;
                    case 119:
                        Map[i] = "p2h";
                        break;
                    case 1599:
                        Map[i] = "p3lt";
                        break;
                    case 1598:
                        Map[i] = "p3l";
                        break;
                    case 1597:
                        Map[i] = "p3h";
                        break;
                    case 1560:
                        Map[i] = "p4ut";
                        break;
                    case 1520:
                        Map[i] = "p4u";
                        break;
                    case 1480:
                        Map[i] = "p4h";
                        break;
                    default:
                        Map[i] = "";
                        break;
                }
            }
            Random rnd = new Random();
            while (appleCounter < 2) {
                int attempt = rnd.Next(1600);
                if (Map[attempt] == "") {
                    Map[attempt] = "a";
                    appleCounter++;
                }
            }
            await Clients.All.SendAsync("GameStart",Map,Users,Directions,Alive);
        }
        public async Task Update() {

            if (Alive[0])
            {
                if (Directions[0] == "w")
                {
                    if (HeadIndex[0] - 40 > 0)
                    {
                        if (Map[HeadIndex[0] - 40].IndexOf("p1") == -1
                            && Map[HeadIndex[0] - 40].IndexOf("p2") == -1
                            && Map[HeadIndex[0] - 40].IndexOf("p3") == -1
                            && Map[HeadIndex[0] - 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[0] - 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[0]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 40] += "t";
                                    TailIndex[0] -= 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 40] += "t";
                                    TailIndex[0] += 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 1] += "t";
                                    TailIndex[0] += 1;
                                }
                                else if (Map[TailIndex[0]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 1] += "t";
                                    TailIndex[0] -= 1;
                                }
                            }
                            else {
                                appleCounter--;
                            }
                            Map[HeadIndex[0] - 40] = "p1h";
                            Map[HeadIndex[0]] = "p1u";
                            HeadIndex[0] -= 40;
                        }
                        else
                        {
                            Alive[0] = false;
                        }
                    }
                    else
                    {
                        Alive[0] = false;
                    }
                }
                else if (Directions[0] == "s")
                {
                    if (HeadIndex[0] + 40 < 1600)
                    {
                        if (Map[HeadIndex[0] + 40].IndexOf("p1") == -1
                            && Map[HeadIndex[0] + 40].IndexOf("p2") == -1
                            && Map[HeadIndex[0] + 40].IndexOf("p3") == -1
                            && Map[HeadIndex[0] + 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[0] + 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[0]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 40] += "t";
                                    TailIndex[0] -= 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 40] += "t";
                                    TailIndex[0] += 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 1] += "t";
                                    TailIndex[0] += 1;
                                }
                                else if (Map[TailIndex[0]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 1] += "t";
                                    TailIndex[0] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[0] + 40] = "p1h";
                            Map[HeadIndex[0]] = "p1d";
                            HeadIndex[0] += 40;
                        }
                        else
                        {
                            Alive[0] = false;
                        }
                    }
                    else
                    {
                        Alive[0] = false;
                    }
                }
                else if (Directions[0] == "d")
                {
                    if (HeadIndex[0] % 40 + 1 < 40)
                    {
                        if (Map[HeadIndex[0] + 1].IndexOf("p1") == -1
                            && Map[HeadIndex[0] + 1].IndexOf("p2") == -1
                            && Map[HeadIndex[0] + 1].IndexOf("p3") == -1
                            && Map[HeadIndex[0] + 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[0] + 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[0]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 40] += "t";
                                    TailIndex[0] -= 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 40] += "t";
                                    TailIndex[0] += 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 1] += "t";
                                    TailIndex[0] += 1;
                                }
                                else if (Map[TailIndex[0]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 1] += "t";
                                    TailIndex[0] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[0] + 1] = "p1h";
                            Map[HeadIndex[0]] = "p1r";
                            HeadIndex[0] += 1;
                        }
                        else
                        {
                            Alive[0] = false;
                        }
                    }
                    else
                    {
                        Alive[0] = false;
                    }
                }
                else if (Directions[0] == "a")
                {
                    if (HeadIndex[0] % 40 - 1 > -1)
                    {
                        if (Map[HeadIndex[0] - 1].IndexOf("p1") == -1
                            && Map[HeadIndex[0] - 1].IndexOf("p2") == -1
                            && Map[HeadIndex[0] - 1].IndexOf("p3") == -1
                            && Map[HeadIndex[0] - 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[0] - 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[0]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 40] += "t";
                                    TailIndex[0] -= 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 40] += "t";
                                    TailIndex[0] += 40;
                                }
                                else if (Map[TailIndex[0]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] + 1] += "t";
                                    TailIndex[0] += 1;
                                }
                                else if (Map[TailIndex[0]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[0]] = "";
                                    Map[TailIndex[0] - 1] += "t";
                                    TailIndex[0] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[0] - 1] = "p1h";
                            Map[HeadIndex[0]] = "p1l";
                            HeadIndex[0] -= 1;
                        }
                        else
                        {
                            Alive[0] = false;
                        }
                    }
                    else
                    {
                        Alive[0] = false;
                    }
                }
            }
            if (Alive[1])
            {
                if (Directions[1] == "w")
                {
                    if (HeadIndex[1] - 40 > 0)
                    {
                        if (Map[HeadIndex[1] - 40].IndexOf("p1") == -1
                            && Map[HeadIndex[1] - 40].IndexOf("p2") == -1
                            && Map[HeadIndex[1] - 40].IndexOf("p3") == -1
                            && Map[HeadIndex[1] - 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[1] - 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[1]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 40] += "t";
                                    TailIndex[1] -= 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 40] += "t";
                                    TailIndex[1] += 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 1] += "t";
                                    TailIndex[1] += 1;
                                }
                                else if (Map[TailIndex[1]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 1] += "t";
                                    TailIndex[1] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[1] - 40] = "p2h";
                            Map[HeadIndex[1]] = "p2u";
                            HeadIndex[1] -= 40;
                        }
                        else
                        {
                            Alive[1] = false;
                        }
                    }
                    else
                    {
                        Alive[1] = false;
                    }
                }
                else if (Directions[1] == "s")
                {
                    if (HeadIndex[1] + 40 < 1600)
                    {
                        if (Map[HeadIndex[1] + 40].IndexOf("p1") == -1
                            && Map[HeadIndex[1] + 40].IndexOf("p2") == -1
                            && Map[HeadIndex[1] + 40].IndexOf("p3") == -1
                            && Map[HeadIndex[1] + 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[1] + 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[1]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 40] += "t";
                                    TailIndex[1] -= 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 40] += "t";
                                    TailIndex[1] += 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 1] += "t";
                                    TailIndex[1] += 1;
                                }
                                else if (Map[TailIndex[1]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 1] += "t";
                                    TailIndex[1] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[1] + 40] = "p2h";
                            Map[HeadIndex[1]] = "p2d";
                            HeadIndex[1] += 40;
                        }
                        else
                        {
                            Alive[1] = false;
                        }
                    }
                    else
                    {
                        Alive[1] = false;
                    }
                }
                else if (Directions[1] == "d")
                {
                    if (HeadIndex[1] % 40 + 1 < 40)
                    {
                        if (Map[HeadIndex[1] + 1].IndexOf("p1") == -1
                            && Map[HeadIndex[1] + 1].IndexOf("p2") == -1
                            && Map[HeadIndex[1] + 1].IndexOf("p3") == -1
                            && Map[HeadIndex[1] + 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[1] + 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[1]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 40] += "t";
                                    TailIndex[1] -= 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 40] += "t";
                                    TailIndex[1] += 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 1] += "t";
                                    TailIndex[1] += 1;
                                }
                                else if (Map[TailIndex[1]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 1] += "t";
                                    TailIndex[1] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[1] + 1] = "p2h";
                            Map[HeadIndex[1]] = "p2r";
                            HeadIndex[1] += 1;
                        }
                        else
                        {
                            Alive[1] = false;
                        }
                    }
                    else
                    {
                        Alive[1] = false;
                    }
                }
                else if (Directions[1] == "a")
                {
                    if (HeadIndex[1] % 40 - 1 > -1)
                    {
                        if (Map[HeadIndex[1] - 1].IndexOf("p1") == -1
                            && Map[HeadIndex[1] - 1].IndexOf("p2") == -1
                            && Map[HeadIndex[1] - 1].IndexOf("p3") == -1
                            && Map[HeadIndex[1] - 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[1] - 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[1]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 40] += "t";
                                    TailIndex[1] -= 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 40] += "t";
                                    TailIndex[1] += 40;
                                }
                                else if (Map[TailIndex[1]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] + 1] += "t";
                                    TailIndex[1] += 1;
                                }
                                else if (Map[TailIndex[1]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[1]] = "";
                                    Map[TailIndex[1] - 1] += "t";
                                    TailIndex[1] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[1] - 1] = "p2h";
                            Map[HeadIndex[1]] = "p2l";
                            HeadIndex[1] -= 1;
                        }
                        else
                        {
                            Alive[1] = false;
                        }
                    }
                    else
                    {
                        Alive[1] = false;
                    }
                }
            }
            if (Alive[2])
            {
                if (Directions[2] == "w")
                {
                    if (HeadIndex[2] - 40 > 0)
                    {
                        if (Map[HeadIndex[2] - 40].IndexOf("p1") == -1
                            && Map[HeadIndex[2] - 40].IndexOf("p2") == -1
                            && Map[HeadIndex[2] - 40].IndexOf("p3") == -1
                            && Map[HeadIndex[2] - 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[2] - 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[2]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 40] += "t";
                                    TailIndex[2] -= 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 40] += "t";
                                    TailIndex[2] += 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 1] += "t";
                                    TailIndex[2] += 1;
                                }
                                else if (Map[TailIndex[2]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 1] += "t";
                                    TailIndex[2] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[2] - 40] = "p3h";
                            Map[HeadIndex[2]] = "p3u";
                            HeadIndex[2] -= 40;
                        }
                        else
                        {
                            Alive[2] = false;
                        }
                    }
                    else
                    {
                        Alive[2] = false;
                    }
                }
                else if (Directions[2] == "s")
                {
                    if (HeadIndex[2] + 40 < 1600)
                    {
                        if (Map[HeadIndex[2] + 40].IndexOf("p1") == -1
                            && Map[HeadIndex[2] + 40].IndexOf("p2") == -1
                            && Map[HeadIndex[2] + 40].IndexOf("p3") == -1
                            && Map[HeadIndex[2] + 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[2] + 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[2]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 40] += "t";
                                    TailIndex[2] -= 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 40] += "t";
                                    TailIndex[2] += 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 1] += "t";
                                    TailIndex[2] += 1;
                                }
                                else if (Map[TailIndex[2]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 1] += "t";
                                    TailIndex[2] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[2] + 40] = "p3h";
                            Map[HeadIndex[2]] = "p3d";
                            HeadIndex[2] += 40;
                        }
                        else
                        {
                            Alive[2] = false;
                        }
                    }
                    else
                    {
                        Alive[2] = false;
                    }
                }
                else if (Directions[2] == "d")
                {
                    if (HeadIndex[2] % 40 + 1 < 40)
                    {
                        if (Map[HeadIndex[2] + 1].IndexOf("p1") == -1
                            && Map[HeadIndex[2] + 1].IndexOf("p2") == -1
                            && Map[HeadIndex[2] + 1].IndexOf("p3") == -1
                            && Map[HeadIndex[2] + 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[2] + 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[2]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 40] += "t";
                                    TailIndex[2] -= 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 40] += "t";
                                    TailIndex[2] += 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 1] += "t";
                                    TailIndex[2] += 1;
                                }
                                else if (Map[TailIndex[2]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 1] += "t";
                                    TailIndex[2] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[2] + 1] = "p3h";
                            Map[HeadIndex[2]] = "p3r";
                            HeadIndex[2] += 1;
                        }
                        else
                        {
                            Alive[2] = false;
                        }
                    }
                    else
                    {
                        Alive[2] = false;
                    }
                }
                else if (Directions[2] == "a")
                {
                    if (HeadIndex[2] % 40 - 1 > -1)
                    {
                        if (Map[HeadIndex[2] - 1].IndexOf("p1") == -1
                            && Map[HeadIndex[2] - 1].IndexOf("p2") == -1
                            && Map[HeadIndex[2] - 1].IndexOf("p3") == -1
                            && Map[HeadIndex[2] - 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[2] - 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[2]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 40] += "t";
                                    TailIndex[2] -= 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 40] += "t";
                                    TailIndex[2] += 40;
                                }
                                else if (Map[TailIndex[2]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] + 1] += "t";
                                    TailIndex[2] += 1;
                                }
                                else if (Map[TailIndex[2]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[2]] = "";
                                    Map[TailIndex[2] - 1] += "t";
                                    TailIndex[2] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[2] - 1] = "p3h";
                            Map[HeadIndex[2]] = "p3l";
                            HeadIndex[2] -= 1;
                        }
                        else
                        {
                            Alive[2] = false;
                        }
                    }
                    else
                    {
                        Alive[2] = false;
                    }
                }
            }
            if (Alive[3])
            {
                if (Directions[3] == "w")
                {
                    if (HeadIndex[3] - 40 > 0)
                    {
                        if (Map[HeadIndex[3] - 40].IndexOf("p1") == -1
                            && Map[HeadIndex[3] - 40].IndexOf("p2") == -1
                            && Map[HeadIndex[3] - 40].IndexOf("p3") == -1
                            && Map[HeadIndex[3] - 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[3] - 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[3]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 40] += "t";
                                    TailIndex[3] -= 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 40] += "t";
                                    TailIndex[3] += 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 1] += "t";
                                    TailIndex[3] += 1;
                                }
                                else if (Map[TailIndex[3]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 1] += "t";
                                    TailIndex[3] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[3] - 40] = "p4h";
                            Map[HeadIndex[3]] = "p4u";
                            HeadIndex[3] -= 40;
                        }
                        else
                        {
                            Alive[3] = false;
                        }
                    }
                    else
                    {
                        Alive[3] = false;
                    }
                }
                else if (Directions[3] == "s")
                {
                    if (HeadIndex[3] + 40 < 1600)
                    {
                        if (Map[HeadIndex[3] + 40].IndexOf("p1") == -1
                            && Map[HeadIndex[3] + 40].IndexOf("p2") == -1
                            && Map[HeadIndex[3] + 40].IndexOf("p3") == -1
                            && Map[HeadIndex[3] + 40].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[3] + 40].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[3]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 40] += "t";
                                    TailIndex[3] -= 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 40] += "t";
                                    TailIndex[3] += 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 1] += "t";
                                    TailIndex[3] += 1;
                                }
                                else if (Map[TailIndex[3]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 1] += "t";
                                    TailIndex[3] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[3] + 40] = "p4h";
                            Map[HeadIndex[3]] = "p4d";
                            HeadIndex[3] += 40;
                        }
                        else
                        {
                            Alive[3] = false;
                        }
                    }
                    else
                    {
                        Alive[3] = false;
                    }
                }
                else if (Directions[3] == "d")
                {
                    if (HeadIndex[3] % 40 + 1 < 40)
                    {
                        if (Map[HeadIndex[3] + 1].IndexOf("p1") == -1
                            && Map[HeadIndex[3] + 1].IndexOf("p2") == -1
                            && Map[HeadIndex[3] + 1].IndexOf("p3") == -1
                            && Map[HeadIndex[3] + 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[3] + 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[3]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 40] += "t";
                                    TailIndex[3] -= 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 40] += "t";
                                    TailIndex[3] += 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 1] += "t";
                                    TailIndex[3] += 1;
                                }
                                else if (Map[TailIndex[3]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 1] += "t";
                                    TailIndex[3] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[3] + 1] = "p4h";
                            Map[HeadIndex[3]] = "p4r";
                            HeadIndex[3] += 1;
                        }
                        else
                        {
                            Alive[3] = false;
                        }
                    }
                    else
                    {
                        Alive[3] = false;
                    }
                }
                else if (Directions[3] == "a")
                {
                    if (HeadIndex[3] % 40 - 1 > -1)
                    {
                        if (Map[HeadIndex[3] - 1].IndexOf("p1") == -1
                            && Map[HeadIndex[3] - 1].IndexOf("p2") == -1
                            && Map[HeadIndex[3] - 1].IndexOf("p3") == -1
                            && Map[HeadIndex[3] - 1].IndexOf("p4") == -1)
                        {
                            if (Map[HeadIndex[3] - 1].IndexOf("a") == -1)
                            {
                                if (Map[TailIndex[3]].IndexOf("u") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 40] += "t";
                                    TailIndex[3] -= 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("d") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 40] += "t";
                                    TailIndex[3] += 40;
                                }
                                else if (Map[TailIndex[3]].IndexOf("r") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] + 1] += "t";
                                    TailIndex[3] += 1;
                                }
                                else if (Map[TailIndex[3]].IndexOf("l") != -1)
                                {
                                    Map[TailIndex[3]] = "";
                                    Map[TailIndex[3] - 1] += "t";
                                    TailIndex[3] -= 1;
                                }
                            }
                            else
                            {
                                appleCounter--;
                            }
                            Map[HeadIndex[3] - 1] = "p4h";
                            Map[HeadIndex[3]] = "p4l";
                            HeadIndex[3] -= 1;
                        }
                        else
                        {
                            Alive[3] = false;
                        }
                    }
                    else
                    {
                        Alive[3] = false;
                    }
                }
            }
            Random rnd = new Random();
            while (appleCounter < 2)
            {
                int attempt = rnd.Next(1600);
                if (Map[attempt] == "")
                {
                    Map[attempt] = "a";
                    appleCounter++;
                }
            }
            
            await Clients.All.SendAsync("RecieveUpdate", Map, Alive);
        }
        public async Task SendData(string user,string data)
        {
            for (int i = 0; i < Users.Length; i++) {
                if (Users[i] == user) {
                    Directions[i] = data;
                    break;
                }
            }
            await Clients.Caller.SendAsync("Accepted");
        }
    }
}
