import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from 'app/common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-create',
    templateUrl: './<#classNameLowerAndSeparator#>-create.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-create.component.css'],
})
export class <#className#>CreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
 
    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {

        this.vm = this.<#classNameInstance#>Service.initVM();
		this.<#classNameInstance#>Service.detectChanges(this.ref);
    }

    ngOnInit() {

       
    }

    onSave(model) {

        this.<#classNameInstance#>Service.save(model).subscribe((result) => {
            this.router.navigate(["/<#classNameLower#>"])
        });
    }

}
