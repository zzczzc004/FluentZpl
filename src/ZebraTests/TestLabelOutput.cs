using System;
using System.Drawing;
using System.Net;
using NUnit.Framework;

namespace ZebraTests
{
    using ZplLabels;
    using ZplLabels.ZPL;

    [TestFixture]
    public class TestLabelOutput
    {
        private ZplLabel _label;
        [OneTimeSetUp]
        public void SetUpForAllTests()
        {

        }
        [SetUp]
        public void SetUpForEachTest()
        {
            _label = new ZplLabel();
        }

        [Test]
        public void can_get_label_script()
        {
            var str = _label.Load(FieldGenFactory.GetText().At(1, 150).SetFont("D", FieldOrientation.Normal, 84).WithData("PO Number").Centered(1200).Underline(),
                FieldGenFactory.GetBarcode().At(1, 250).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 48).WithData("22343").Height(150).BarWidth(4).Centered(1200),
                FieldGenFactory.GetText().At(1, 500).SetFont("D", FieldOrientation.Normal, 56).WithData("PO Line Number").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 550).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 40).WithData("1").Height(70).BarWidth(2).Centered(1200),
                FieldGenFactory.GetText().At(1, 740).SetFont("D", FieldOrientation.Normal, 56).WithData("Stryker Part Number").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 820).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 72).WithData("0682001252").Height(80).BarWidth(3).Centered(1200),
                FieldGenFactory.GetText().At(1, 1050).SetFont("D", FieldOrientation.Normal, 72).WithData("Serial/Lot Number").Centered(1200).Underline(),
                FieldGenFactory.GetBarcode().At(1, 1120).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 48).WithData("10000000006898").BarWidth(4).Height(110).Centered(1200),
                FieldGenFactory.GetText().At(1, 1320).SetFont("D", FieldOrientation.Inverted, 64).WithData("QTY").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 1400).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Inverted, 72).WithData("10").BarWidth(4).Height(150).Centered(1200),
                FieldGenFactory.GetStringGraphic().At(1, 1800).WithData("中文测试").Centered(1200).SetFont(new Font("宋体", 72))
                ).ToString();
            Console.Write(str); 
            var addr = new byte[] { 1, 0, 0, 127 };
            var conn = new NetworkPrinter();
            conn.Print(new IPAddress(addr), str);
        }

        [Test]
        public void print_chinese_lable_script()
        {
            var _labelWidth = 240; //30mm*8
            var zpl = _label.Load(
                    FieldGenFactory.GetStringGraphic()
                        .At(0, 10)
                        .WithData("中文测试")
                        .Centered(_labelWidth)
                        .SetFont(new Font("宋体", 14)),
                    FieldGenFactory.GetBarcode().At(0, 50).WithData("20161104090000").SetBarcodeType(BarcodeType.Code128).
                        SetFont("D", FieldOrientation.Normal, 18).BarWidth(1).Height(40).Centered(_labelWidth))
                    .ToString();

            RawPrinterHelper.SendStringToPrinter("ZDesigner GK888t", zpl);
        }

        [TearDown]
        public void TearDownForEachTest()
        {

        }

        [OneTimeTearDown]
        public void TearDownAfterAllTests()
        {

        }
    }
}
