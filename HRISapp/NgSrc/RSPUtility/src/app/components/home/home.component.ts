import { Component, OnInit, ViewChild } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { FormControl } from "@angular/forms";
import { SwalComponent, SwalPartialTargets } from '@sweetalert2/ngx-sweetalert2';
import { Select2OptionData } from "ng2-select2";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  public employeeData: Array<Select2OptionData>;

  @ViewChild("SwalMigratedProfile", { static: false})  SwalMigratedProfile: SwalComponent;

  myControl = new FormControl;
  options: string[] = ['One', 'Two', 'Three'];

  Employees: any;
  EmployeeProfile: any;
  source: any;
  confirmed: any;

  selectedEmployeeEIC: any;

  constructor(
    private http: HttpClient,
    private swal: SwalPartialTargets
  ) { }

  ngOnInit() {
    this.FetchOldEmployees();
  }

  /**
   * MigrateEmployee
   */
  public MigrateEmployee() {

    let url = "/RSPUtility/PostEmployee";

    let body = {
      "EIC": this.selectedEmployeeEIC
    }

    this.http.post(url, body).toPromise().then(
      employee => {
        this.EmployeeProfile = employee;
        console.log(this.EmployeeProfile);
        
        this.SwalMigratedProfile.show();

        this.FetchOldEmployees();
        
      },
      reason => {
        console.log(reason.error);
      }
    );
    
  }

  /**
   * selectedType
   */
  public selectedEmployee(paramValue) {
    this.selectedEmployeeEIC = paramValue;
  }

  public selectedEmployee2(data) {
    console.log(data);
    
    this.selectedEmployeeEIC = data.value;
  }

  /**
   * FetchOldEmployees
   */
  public FetchOldEmployees() {

    let url = "/RSPUtility/OldEmployees"
    this.http.get(url).toPromise().then(
      employees => {
        this.Employees = employees;
        this.employeeData = this.Employees;        
      },
      reason => {
        console.log(reason.error);
        
      }
    );
    
  }

}
