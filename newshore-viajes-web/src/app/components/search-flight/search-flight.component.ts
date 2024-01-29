import { CommonModule } from '@angular/common';
import { Component, NgModule, OnInit } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { SearchRequest } from 'src/app/models/search-request'
import { SearchResponse } from 'src/app/models/search-response'
import { NewshoreViajesApiService } from 'src/app/services/newshore-viajes-api.service'
import { ExchanGerateApiService } from 'src/app/services/exchangerate-api.service'
import { ConversionRates } from 'src/app/models/conversion-rates'

@Component({
  selector: 'app-search-flight',
  templateUrl: './search-flight.component.html',
  styleUrls: ['./search-flight.component.css']
})
export class SearchFlightComponent implements OnInit {
  title = 'newshore-viajes-web';
  formSearch!: FormGroup;
  formCurrency!: FormGroup;
  searchRequest: SearchRequest = new SearchRequest()
  searchResponse: SearchResponse = new SearchResponse();
  viewError: boolean = false;
  viewResult: boolean = false;
  errorMessage: string = '';
  conversionRates: ConversionRates = new ConversionRates();
  currencySelected  = 'USD';

  constructor(
    private fb: FormBuilder,
    private newshoreViajesApiService: NewshoreViajesApiService,
    private exchanGerateApiService: ExchanGerateApiService ) { }

  ngOnInit(): void {
    this.createForm();
  }

  createForm() {
    this.formSearch = this.fb.group({
      origin: new FormControl('', [
        Validators.required,
        Validators.maxLength(3),
        Validators.minLength(3)
      ]),
      destination: new FormControl('', [
        Validators.required,
        Validators.maxLength(3),
        Validators.minLength(3)
      ])
    })

    this.formCurrency = this.fb.group({
      currency: new FormControl('', [
        Validators.required
      ])
    })
  }

  validateOriginAndDestintation()
  {
    return this.formSearch.controls['origin'].value == this.formSearch.controls['destination'].value
  }

  search() {
    // this.formCurrency.controls['currency'].value. = '';
    this.searchRequest.origin = this.formSearch.controls['origin'].value;
    this.searchRequest.destination = this.formSearch.controls['destination'].value;
    this.newshoreViajesApiService.Search( this.searchRequest).subscribe(response => {
      this.searchResponse = response  as SearchResponse;
      this.viewError = false;
      this.viewResult = true;
    }, (httpErrorResponse ) => {
      console.log(httpErrorResponse.error.Message);
      this.errorMessage = httpErrorResponse.error.Message;
      this.viewError = true;
      this.viewResult = false; 
    });
  }

  calculateChangeDivisa(value: any)
  {
    this.currencySelected = value.target.value;
    this.getExchangeDivisa()
  }

  getExchangeDivisa()
  {
    var amount: number = this.searchResponse.price;
    this.exchanGerateApiService.getConversionRates('USD',this.currencySelected, amount).subscribe(response => {
      this.conversionRates = response  as ConversionRates;
      console.log(this.conversionRates);
      this.searchResponse.priceCurrencyChange = this.conversionRates.conversion_result;
    }, (httpErrorResponse ) => {
      console.log(httpErrorResponse);
    });
  }

}

const routes: Routes = [
  { path: '', component: SearchFlightComponent },
];

@NgModule({
  declarations: [
    SearchFlightComponent
  ],
  exports: [
    SearchFlightComponent
  ],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    ReactiveFormsModule
  ]
})

export class SearchFlightModule {}
