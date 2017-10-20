import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from 'app/common/model/viewmodel';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-field-filter',
    templateUrl: './<#classNameLowerAndSeparator#>-field-filter.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-field-filter.component.css']
})
export class <#className#>FieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
