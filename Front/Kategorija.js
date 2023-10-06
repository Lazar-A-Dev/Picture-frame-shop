export class Kategorija{
    constructor(id, nazivKategorije){
        this.id = id;
        this.nazivKategorije = nazivKategorije;

        this.listaSlika = [];
        this.container = null;
    }

    dodajListiSlika(slika){
        this.listaSlika.push(slika);
    }
}