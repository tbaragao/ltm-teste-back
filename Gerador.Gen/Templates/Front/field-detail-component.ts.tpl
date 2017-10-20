
import { Component, OnInit, Input } from '@angular/core';
import { ViewModel } from 'app/common/model/viewmodel';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-field-details',
    templateUrl: './<#classNameLowerAndSeparator#>-field-details.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-field-details.component.css']
})
export class <#className#>FieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
