using Microsoft.AspNetCore.Mvc;
using PP2.Web.Models;
using System.Collections.Generic;

namespace PP2.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index() => View(new BinaryCalcViewModel());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(BinaryCalcViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            int a = BinaryCalcViewModel.BinToInt(vm.A);
            int b = BinaryCalcViewModel.BinToInt(vm.B);

            string andBin = BinaryCalcViewModel.BitwiseAnd(vm.A, vm.B);
            string orBin  = BinaryCalcViewModel.BitwiseOr(vm.A, vm.B);
            string xorBin = BinaryCalcViewModel.BitwiseXor(vm.A, vm.B);

            int sum = a + b;
            int mul = a * b;

            vm.Results = new List<ResultRow>
            {
                Row("a",        vm.A8, a),
                Row("b",        vm.B8, b),
                Row("a AND b",  andBin, BinaryCalcViewModel.BinToInt(andBin)),
                Row("a OR b",   orBin,  BinaryCalcViewModel.BinToInt(orBin)),
                Row("a XOR b",  xorBin, BinaryCalcViewModel.BinToInt(xorBin)),
                Row("a + b",    BinaryCalcViewModel.IntToBin(sum), sum),
                Row("a • b",    BinaryCalcViewModel.IntToBin(mul), mul),
            };
            return View(vm);
        }

        private static ResultRow Row(string label, string bin, int value) => new()
        {
            Label = label,
            Bin = bin,
            Oct = BinaryCalcViewModel.IntToOct(value),
            Dec = BinaryCalcViewModel.IntToDec(value),
            Hex = BinaryCalcViewModel.IntToHex(value)
        };
    }
}
