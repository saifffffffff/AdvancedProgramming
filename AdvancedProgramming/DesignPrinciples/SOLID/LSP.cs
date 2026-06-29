using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace AdvancedProgramming.DesignPrinciples.SOLID;

internal class LSP
{
    // before liskov substitution
    interface IUser
    {
        
        void SetName(string name);
        int getId(string name);
    }

    class Admin : IUser
    {
        string _name;

        public int getId(string name)
        {
            return 10;
        }

        public void SetName(string name)
        {
            _name = _name.ToUpper(); // this behaves differently than it is supposed to be 
        }




    }


    // after liskov substitution 

    interface IUser_
    {
        void SetName(string name);
        int getId(string name);
    }

    class Admin_ : IUser_
    {

        string _name;
        public int getId(string name)
        {
            int id = 10;//_repo.GetIdByName(name);
            return id;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }


}
