export class Materijal{
    constructor(id, nazivMaterijala){
        this.id = id;
        this.nazivMaterijala = nazivMaterijala;

        this.listaSlika = [];
        this.container = null;
    }

    dodajListiSlika(slika){
        this.listaSlika.push(slika);
    }
}