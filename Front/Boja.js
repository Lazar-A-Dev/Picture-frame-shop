export class Boja{
    constructor(id, nazivBoje){
        this.id = id;
        this.nazivBoje = nazivBoje;

        this.listaSlika = [];
        this.container = null;
    }

    dodajListiSlika(slika){
        this.listaSlika.push(slika);
    }
}