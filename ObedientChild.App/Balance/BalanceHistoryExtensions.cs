using ObedientChild.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObedientChild.App.Balance
{
    internal static class BalanceHistoryExtensions
    {
        public static BalanceHistoryProps CloneProps(this BalanceHistory model)
        {
            var props = new BalanceHistoryProps();

            props.UserId = model.UserId;
            props.EntityId = model.EntityId;
            props.Amount = model.Amount;
            props.Title = model.Title;
            props.ImageUrl = model.ImageUrl;
            props.Type = model.Type;
            props.DateTime = model.DateTime;
            props.BalanceType = model.BalanceType;

            return props;
        }

        public static BalanceHistory ToModel(this BalanceHistoryProps props)
        {
            var model = new BalanceHistory();

            model.UserId = props.UserId;
            model.EntityId = props.EntityId;
            model.Amount = props.Amount;
            model.Title = props.Title;
            model.ImageUrl = props.ImageUrl;
            model.Type = props.Type;
            model.DateTime = props.DateTime;
            model.BalanceType = props.BalanceType;

            return model;
        }
    }
}
