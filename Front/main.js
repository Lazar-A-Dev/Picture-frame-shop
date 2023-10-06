import { Prodavnica } from "./Prodavnica.js";
import { Slika } from "./Slika.js";
import { Boja } from "./Boja.js";
import { Materijal } from "./Materijal.js";
import { Kategorija } from "./Kategorija.js";

var response = await fetch("http://localhost:5020/Prodavnica/VratiSveProdavnice");
var data = await response.json();

data.forEach(async p => {
    var prodavnica = new Prodavnica(p["id"]);

    var boje = p["boje"];
    var kategorije = p["kategorije"];
    var materijali = p["materijali"];

    boje.forEach(async b => {
        var boja = new Boja(b["id"], b["nazivBoje"]);
        //console.log(boja);
        prodavnica.dodajListiBoja(boja);
    });

    kategorije.forEach(async k => {
        var kat = new Kategorija(k["id"], k["nazivKategorije"]);
        //console.log(kat);
        prodavnica.dodajListiKat(kat);
    });

    materijali.forEach(async m => {
        var mat = new Materijal(m["id"], m["nazivMaterijala"]);
        //console.log(mat);
        prodavnica.dodajListiMat(mat);
    });

    prodavnica.crtajProdavnicu(document.body);
});