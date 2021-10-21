using ClosedXML.Excel;
using DataBase.Entities;
using DataBase.Repositroy.Interface;
using ServiceLibrary.Service.Interface;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary.Service
{
    public class SelectionService : ISelectionService
    {
        private readonly ISelectionRepository _selectionRepository;
        public SelectionService(ISelectionRepository selectionRepository)
        {
            _selectionRepository = selectionRepository;
        }
        public Task CreateSelection(Selection selection, int[] param)
        {
            string selectionString = $"SELECT TOP(5) us.*, ev1.total \n" +
                                     $"from Users us \n" +
                                    $"inner join(select ev.UserId, sum(ev.Mark* par.Coefficient) as total \n" +
                                    $"from Evaluations ev \n" +
                                    $"inner join Parametrs par on par.Id = ParameterId \n";
            for (int i = 0; i < param.Length; i++)
            {
                if (i == 0)
                    selectionString += $"where ev.ParameterId = {param[i]}";
                else if (i == param.Length - 1)
                    selectionString += $" or ev.ParameterId = {param[i]} \n and DATEDIFF(MONTH,ev.AssessmentDate,GETDATE()) < 3";
                else
                    selectionString += $" or ev.ParameterId = {param[i]}";
            }
            selectionString += $"group by ev.UserId) as ev1 on us.Id = ev1.UserId \n" +
                               $"order by ev1.total DESC \n";
            selection.SelectionQuery = selectionString;
            return _selectionRepository.CreateSelection(selection);
        }

        public IEnumerable<User> GetUsers(int id)
        {
            return _selectionRepository.GetUsers(id);
        }

        public Selection GetSelection(int id)
        {
            return _selectionRepository.Selections().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<Selection> GetSelectionsFromDepartment(int id)
        {
            return _selectionRepository.GetSelectionsFromDepartment(id);
        }

        public IEnumerable<Selection> Selections()
        {
            return _selectionRepository.Selections();
        }

        public byte[] ExportSelection(Selection selection)
        {
            var evaluation = _selectionRepository.GetUsers(selection.Id);
            var users  = evaluation;
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Id";
                worksheet.Cell(currentRow, 2).Value = "Login";
                worksheet.Cell(currentRow, 3).Value = "Role";
                worksheet.Cell(currentRow, 4).Value = "Parametr";

                foreach (var user in users)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = user.Id;
                    worksheet.Cell(currentRow, 2).Value = user.Login;
                    worksheet.Cell(currentRow, 3).Value = user.Role.RoleName;
                    worksheet.Cell(currentRow, 4).Value = user.Department.DepartmentName;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }
    }
}
