import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ExchanGerateApiService {
  private urlBase = 'https://v6.exchangerate-api.com/v6/a5b7948fb97ad7a296abe855/pair/';

  constructor(
    private httpClient: HttpClient
  ) { }

  getConversionRates(baseCurrency: string, toCurrency: string, amount: number)
  {
    return this.httpClient.get(`${this.urlBase + baseCurrency + '/' + toCurrency + '/' + amount }`);
  }
}
