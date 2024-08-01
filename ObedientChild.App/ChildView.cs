using ObedientChild.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.App
{
    public class ChildView
    {
        private readonly Child _model;

        public int Id => _model.Id;

        public string Name => _model.Name;

        public byte[] Avatar => _model.Avatar;

        public int Balance => _model.Balance;

        public int BigGoalId => _model.BigGoalId;

        public int BigGoalBalance => _model.BigGoalBalance;

        public int? DreamId => _model.DreamId;

        public int DreamBalance => _model.DreamBalance;

        public List<ChildStatus> Statuses { get; set; }

        public ChildView(Child model)
        {
            _model = model;
        }
    }
}
