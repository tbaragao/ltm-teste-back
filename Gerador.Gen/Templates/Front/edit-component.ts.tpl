import { Component, OnInit, Input,ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from 'app/common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';
import { GlobalService, NotificationParameters} from '../../../global.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-edit',
    templateUrl: './<#classNameLowerAndSeparator#>-edit.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-edit.component.css'],
})
export class <#className#>EditComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {

		 this.vm = null;

    }

    ngOnInit() {

		this.vm = this.<#classNameInstance#>Service.initVM();
		this.<#classNameInstance#>Service.detectChanges(this.ref);

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

		setTimeout(() => {
        this.<#classNameInstance#>Service.get({ id: this.id }).subscribe((data) => {
            this.vm.model = data.data;
			GlobalService.getNotificationEmitter().emit(new NotificationParameters("edit", {
                model: this.vm.model
            }));
        })}, 250);

    }

    onSave(model) {

        this.<#classNameInstance#>Service.save(model).subscribe((result) => {
            this.router.navigate(["/<#classNameLower#>"])
        });
    }

}
