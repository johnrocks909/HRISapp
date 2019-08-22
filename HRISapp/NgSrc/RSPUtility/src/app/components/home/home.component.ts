import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { FormControl } from "@angular/forms";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  myControl = new FormControl;
  options: string[] = ['One', 'Two', 'Three'];

  Employees: any;
  source: any;
  confirmed: any;

  SelectedType: any;

  constructor(
    private router: Router,
    private http: HttpClient
  ) { }

  ngOnInit() {
    // this.FetchOldEmployees();
    // console.log(this.source);

    // this.router.navigate(["ci"]);

    let url = "/RSPUtility/OldEmployees"
    this.http.get(url).toPromise().then(
      employees => {
        this.Employees = employees;
      },
      reason => {
        console.log(reason.error);
        
      }
    );
    
  }

  /**
   * selectedType
   */
  public selectedType(paramValue) {
    this.SelectedType = paramValue;    
  }

  /**
   * FetchOldEmployees
   */
  public FetchOldEmployees() {
    
    this.source = [
      {
        "key": 1,
        "station": "Antonito",
        "state": "CO"
      },
      {
        "key": 2,
        "station": "Big Horn",
        "state": "NM"
      },
      {
        "key": 3,
        "station": "Sublette",
        "state": "NM"
      },
      {
        "key": 4,
        "station": "Toltec",
        "state": "NM"
      },
      {
        "key": 5,
        "station": "Osier",
        "state": "CO"
      },
      {
        "key": 6,
        "station": "Chama",
        "state": "NM"
      },
      {
        "key": 7,
        "station": "Monero",
        "state": "NM"
      },
      {
        "key": 8,
        "station": "Lumberton",
        "state": "NM"
      },
      {
        "key": 9,
        "station": "Duice",
        "state": "NM"
      },
      {
        "key": 10,
        "station": "Navajo",
        "state": "NM"
      },
      {
        "key": 11,
        "station": "Juanita",
        "state": "CO"
      },
      {
        "key": 12,
        "station": "Pagosa Jct",
        "state": "CO"
      },
      {
        "key": 13,
        "station": "Carracha",
        "state": "CO"
      },
      {
        "key": 14,
        "station": "Arboles",
        "state": "CO"
      },
      {
        "key": 15,
        "station": "Solidad",
        "state": "CO"
      },
      {
        "key": 16,
        "station": "Tiffany",
        "state": "CO"
      },
      {
        "key": 17,
        "station": "La Boca",
        "state": "CO"
      },
      {
        "key": 18,
        "station": "Ignacio",
        "state": "CO"
      },
      {
        "key": 19,
        "station": "Oxford",
        "state": "CO"
      },
      {
        "key": 20,
        "station": "Florida",
        "state": "CO"
      },
      {
        "key": 21,
        "station": "Bocea",
        "state": "CO"
      },
      {
        "key": 22,
        "station": "Carbon Jct",
        "state": "CO"
      },
      {
        "key": 23,
        "station": "Durango",
        "state": "CO"
      },
      {
        "key": 24,
        "station": "Home Ranch",
        "state": "CO"
      },
      {
        "key": 25,
        "station": "Trimble Springs",
        "state": "CO"
      },
      {
        "key": 26,
        "station": "Hermosa",
        "state": "CO"
      },
      {
        "key": 27,
        "station": "Rockwood",
        "state": "CO"
      },
      {
        "key": 28,
        "station": "Tacoma",
        "state": "CO"
      },
      {
        "key": 29,
        "station": "Needleton",
        "state": "CO"
      },
      {
        "key": 30,
        "station": "Elk Park",
        "state": "CO"
      },
      {
        "key": 31,
        "station": "Silverton",
        "state": "CO"
      },
      {
        "key": 32,
        "station": "Eureka",
        "state": "CO"
      }
    ];
  }

}
