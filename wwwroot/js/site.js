// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const realFileBtn = document.getElementById("real-file");
const customBtn = document.getElementById("custom-button");
const customTxt = document.getElementById("custom-text");

customBtn.addEventListener("click", function () {
  realFileBtn.click();
});

realFileBtn.addEventListener("change", function () {
  if (realFileBtn.value) {
    customTxt.innerHTML = realFileBtn.value.match(
      /[\/\\]([\w\d\s\.\-\(\)]+)$/
    )[1];
  } else {
    customTxt.innerHTML = "No file chosen, yet.";
  }
});

const $btnExportar = document.querySelector("#btnExportar"),
  $tabla = document.querySelector("#tabla1");

$btnExportar.addEventListener("click", function () {
  let tableExport = new TableExport($tabla, {
    exportButtons: false, // No queremos botones
    filename: "Mi tabla de Excel", //Nombre del archivo de Excel
    sheetname: "Mi tabla de Excel", //Título de la hoja
  });
  let datos = tableExport.getExportData();
  let preferenciasDocumento = datos.tabla.xlsx;
  tableExport.export2file(
    preferenciasDocumento.data,
    preferenciasDocumento.mimeType,
    preferenciasDocumento.filename,
    preferenciasDocumento.fileExtension,
    preferenciasDocumento.merges,
    preferenciasDocumento.RTL,
    preferenciasDocumento.sheetname
  );
});

$(document).ready(function () {
  $("#example").DataTable({
    dom: "Bfrtip",
    buttons: ["copy", "csv", "excel", "pdf", "print"],
  });
});
