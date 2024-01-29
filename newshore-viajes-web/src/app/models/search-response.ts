export class SearchResponse {
    origin: string = '';
    destination: string = '';
    price: number = 0;
    priceCurrencyChange = 0;
    flights: Flight[];
    constructor() {
        this.flights = [];
    }
}

export class Flight {
    transport: Transport;
    origin: string = '';
    destination: string = '';
    price: number = 0;
    constructor() {
        this.transport = new Transport();
    }
}

export class Transport {
    flightCarrier: string = '';
    flightNumber: string = '';
}

