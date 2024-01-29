import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { SearchRequest } from 'src/app/models/search-request'

@Injectable({
  providedIn: 'root'
})
export class NewshoreViajesApiService {
  private urlBase = 'https://localhost:7088/api/flights';

  constructor(
    private httpClient: HttpClient
  ) { }

  // Buscar rutas de vuelos
  Search(searchRequest:SearchRequest)
  {
    return this.httpClient.post(`${this.urlBase}`, searchRequest);
  }
}
