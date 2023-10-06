export class Prodavnica{
    constructor(id){
        this.id = id;

        this.listaBoja = [];
        this.listaMat = [];
        this.listaKat = [];

        this.container = null;
    }

    dodajListiBoja(boja){
        this.listaBoja.push(boja);
    }

    dodajListiMat(mat){
        this.listaMat.push(mat);
    }

    dodajListiKat(kat){
        this.listaKat.push(kat);
    }

    crtajProdavnicu(host){
        this.container = document.createElement("div");
        this.container.className = "glavni";
        host.appendChild(this.container);

        var levi = document.createElement("div");
        levi.className = "levi";
        this.container.appendChild(levi);

        var desni = document.createElement("div");
        desni.className = "desni";
        this.container.appendChild(desni);

        var red1 = document.createElement("div");
        red1.className = "red1";
        levi.appendChild(red1);

        var katLabel = document.createElement("label");
        katLabel.className = "katLabel";
        katLabel.innerHTML = "Kategorija rama: ";
        red1.appendChild(katLabel);

        var katSelect = document.createElement("select");
        katSelect.className = "katSelect";
        red1.appendChild(katSelect);

        this.listaKat.forEach((kat, index) => {
            const option = document.createElement("option");
            option.text = kat.nazivKategorije;
            option.value = index + 1;
            katSelect.appendChild(option);
        });

        var red2 = document.createElement("div");
        red2.className = "red2";
        levi.appendChild(red2);

        var bojaLabel = document.createElement("label");
        bojaLabel.className = "bojaLabel";
        bojaLabel.innerHTML = "Boja rama: ";
        red2.appendChild(bojaLabel);

        var bojaSelect = document.createElement("select");
        bojaSelect.className = "bojaSelect";
        red2.appendChild(bojaSelect);

        this.listaBoja.forEach((boja, index) => {
            const option = document.createElement("option");
            option.text = boja.nazivBoje;
            option.value = index + 1;
            bojaSelect.appendChild(option);
        });


        var red3 = document.createElement("div");
        red3.className = "red3";
        levi.appendChild(red3);

        var matLabel = document.createElement("label");
        matLabel.className = "matLabel";
        matLabel.innerHTML = "Materijal rama: ";
        red3.appendChild(matLabel);

        var matSelect = document.createElement("select");
        matSelect.className = "matSelect";
        red3.appendChild(matSelect);

        this.listaMat.forEach((mat, index) => {
            const option = document.createElement("option");
            option.value = index + 1;
            option.text = mat.nazivMaterijala;
            matSelect.appendChild(option);
        });


        var red4 = document.createElement("div");
        red4.className = "red4";
        levi.appendChild(red4);

        var cenaOdLabel = document.createElement("label");
        cenaOdLabel.innerHTML = "Cena od: ";
        cenaOdLabel.className = "cenaOdLabel";
        red4.appendChild(cenaOdLabel);

        var cenaOdInput = document.createElement("input");
        cenaOdInput.className = "cenaOdInput";
        cenaOdInput.type = "number";
        cenaOdInput.min = 1;
        red4.appendChild(cenaOdInput);


        var red5 = document.createElement("div");
        red5.className = "red5";
        levi.appendChild(red5);

        var cenaDoLabel = document.createElement("label");
        cenaDoLabel.innerHTML = "Cena do: ";
        cenaDoLabel.className = "cenaDoLabel";
        red5.appendChild(cenaDoLabel);

        var cenaDoInput = document.createElement("input");
        cenaDoInput.className = "cenaDoInput";
        cenaDoInput.type = "number";
        cenaDoInput.min = 1;
        red5.appendChild(cenaDoInput);

        var pronadjiBtn = document.createElement("button");
        pronadjiBtn.innerHTML = "Pronadji";
        pronadjiBtn.className = "pronadjiBtn";
        levi.appendChild(pronadjiBtn);

        pronadjiBtn.onclick = (ev) => {
            this.prikaziSlike(desni);
        }
    }

    async prikaziSlike(desni){
        desni.innerHTML = '';
        var kat = parseInt(this.container.querySelector(".katSelect").value);
        var mat = parseInt(this.container.querySelector(".matSelect").value);
        var boja = parseInt(this.container.querySelector(".bojaSelect").value);
        var cenaOd = parseInt(this.container.querySelector(".cenaOdInput").value);
        var cenaDo = parseInt(this.container.querySelector(".cenaDoInput").value);
        //console.log(cenaOd + "|" + cenaDo);

        var response = await fetch(`http://localhost:5020/Slika/VratiTrazeneSlike/${boja}/${kat}/${mat}/${cenaOd}/${cenaDo}`);
        var data = await response.json();
        console.log(data);

        data.forEach(e => {
            var kartica = document.createElement("div");
            kartica.className = "kartica";
            desni.appendChild(kartica);

            var red1K = document.createElement("div");
            red1K.className = "red1K";
            kartica.appendChild(red1K);

            var sifraLabel = document.createElement("label");
            sifraLabel.innerHTML = "Sifra: " + e.sifra;
            red1K.appendChild(sifraLabel);

            var red2K = document.createElement("div");
            red2K.className = "red2K";
            kartica.appendChild(red2K);

            var slika = document.createElement("img");
            slika.className = "slika";
            slika.src = `www.root/${e.nazivSlike}`
            //slika.alt = `www.root/${e.nazivSlike}`
            red2K.appendChild(slika);

            var red3K = document.createElement("div");
            red3K.className = "red3K";
            kartica.appendChild(red3K);

            var velSlikeLabel = document.createElement("label");
            velSlikeLabel.innerHTML = "Velicina slike: " + e.velicnaSlike;
            velSlikeLabel.className = "velSlikeLabel";
            red3K.appendChild(velSlikeLabel);

            var red4K = document.createElement("div");
            red4K.className = "red4K";
            kartica.appendChild(red4K);

            var cenaSlike = document.createElement("label");
            cenaSlike.innerHTML = "Cena: " + e.cena;
            cenaSlike.className = "cenaSlike";
            red4K.appendChild(cenaSlike);

            var red5K = document.createElement("div");
            red5K.className = "red5K";
            kartica.appendChild(red5K);

            var kolicinaLabel = document.createElement("label");
            kolicinaLabel.innerHTML = "Kolicina: " + e.kolicina;
            kolicinaLabel.className = "kolicinaLabel";
            red5K.appendChild(kolicinaLabel);

            var poruciBtn = document.createElement("button");
            poruciBtn.innerHTML = "Poruci";
            poruciBtn.className = "poruciBtn";
            kartica.appendChild(poruciBtn);

            poruciBtn.onclick = (ev) => {
                this.poruciSliku(e.id, kolicinaLabel);
            }

        });
    }

    async poruciSliku(idSlike, kolicinaLabel){
        console.log("Idslike" + idSlike);
        var response = await fetch(`http://localhost:5020/Slika/PoruciSliku/${idSlike}`, {
            method: "PUT"
        });
        //var data = await response.json();

        var response2 = await fetch(`http://localhost:5020/Slika/VratiJednuSlike/${idSlike}`);
        var data2 = await response2.json();

        kolicinaLabel.innerHTML = "Kolicina: " + data2.kolicina;
    }   
}