using System;
using System.Windows.Forms;
using DAL.Service;
using EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EntitiFramework
{
    public partial class Form1 : Form
    {
        private FootballService _footballService;
        public Form1()
        {
            InitializeComponent();
            var optionsBuilder = new DbContextOptionsBuilder<AppDBcontext>();
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Football;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            _footballService = new FootballService(optionsBuilder.Options);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
