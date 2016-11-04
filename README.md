FluentZpl
=========

<h2>A fluent interface to build labels using ZPL</h2>

FluentZpl consists of an assembly called ZplLabels that allows creation and printing of Zebra labels through a fluent interface. The ZplLabel class enables creation of label scripts through its Load() method, and either PrinterConnection or LabelPrinter can be used to send the resulting script to a Zebra printer.

The ZplLabel.Load() method takes an array of IFieldGenerator objects as parameters.  The field generators are created through static methods of the FieldGenFactory class. 

[Online ZPL Viewer](http://labelary.com/viewer.html) is a useful website for Zpl develop.

<h2>Creating a text field:</h2>

<pre>FieldGenFactory.GetText().At(1, 500).SetFont(Fonts.D, FieldOrientation.Normal, 56).WithData("PO Line Number").Centered(1200)
</pre>
<h2>Creating a barcode field:</h2>
<pre>
 FieldGenFactory.GetBarcode().At(1, 550).SetBarcodeType(BarcodeType.Code128).SetFont(Fonts.D, FieldOrientation.Normal, 40).WithData("1").Height(70).BarWidth(2).Centered(1200) </pre>
<h2>Creating a text field which contained chinese character :</h2>
<pre>
 FieldGenFactory.GetStringGraphic().At(0, 10).WithData("中文测试").Centered(_labelWidth).SetFont(new Font("宋体", 14) </pre>
 
<h2>To create a complete label:</h2>

<pre>var label  = _label.Load(FieldGenFactory.GetText().At(1, 150).SetFont("D", FieldOrientation.Normal, 84).WithData("PO Number").Centered(1200).Underline(),
                FieldGenFactory.GetBarcode().At(1, 250).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 48).WithData("22343").Height(150).BarWidth(4).Centered(1200),
                FieldGenFactory.GetText().At(1, 500).SetFont("D", FieldOrientation.Normal, 56).WithData("PO Line Number").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 550).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 40).WithData("1").Height(70).BarWidth(2).Centered(1200),
                FieldGenFactory.GetText().At(1, 740).SetFont("D", FieldOrientation.Normal, 56).WithData("Stryker Part Number").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 820).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 72).WithData("0682001252").Height(80).BarWidth(3).Centered(1200),
                FieldGenFactory.GetText().At(1, 1050).SetFont("D", FieldOrientation.Normal, 72).WithData("Serial/Lot Number").Centered(1200).Underline(),
                FieldGenFactory.GetBarcode().At(1, 1120).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Normal, 48).WithData("10000000006898").BarWidth(4).Height(110).Centered(1200),
                FieldGenFactory.GetText().At(1, 1320).SetFont("D", FieldOrientation.Inverted, 64).WithData("QTY").Centered(1200),
                FieldGenFactory.GetBarcode().At(1, 1400).SetBarcodeType(BarcodeType.Code128).SetFont("D", FieldOrientation.Inverted, 72).WithData("10").BarWidth(4).Height(150).Centered(1200),
                FieldGenFactory.GetStringGraphic().At(1, 1800).WithData("中文测试").Centered(1200).SetFont(new Font("宋体", 72)))</pre>
                
The "ToString()" method produces the following script.

![PreviewBarCode](/docs/images/preview-barcode.png)