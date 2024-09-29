// mko, 29.9.2024
// Definition aller eingesetzer Endpunkte

export class Endpoints {

    serverOrigin: string;

    constructor(serverOrigin: string) {
        this.serverOrigin = serverOrigin;
    }

    UrlForGetNamingContainers(rootNameSpace: string) : string {
        return `${this.serverOrigin}/NamingContainers?NC=${rootNameSpace}`;
    }

    UrlForDownloadFromFileStore(fileName) : string {
        return `${this.serverOrigin}/fileStore?fileName=${fileName}`;
    }

}