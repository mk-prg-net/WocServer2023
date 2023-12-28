export class NytKeyword {

    constructor(uniCode: string, htmlEntity: string) {
        this.HtmlEntity = htmlEntity;
        this.UniCode = uniCode;
    }

    public UniCode: string;
    public HtmlEntity: string;
}