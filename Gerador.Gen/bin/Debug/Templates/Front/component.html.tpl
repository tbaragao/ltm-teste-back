<div class="gc-body__heading">
  <ol class="breadcrumb">
    <li class="breadcrumb-item"><a href="/">Home</a></li>
  </ol>

  <div class="gc-heading__controls">
    <div class="container-fluid">
      <div class="row">
        <div class="col d-flex justify-content-end">
          <a href="javascript:history.back()" class="btn btn-sm btn-outline-secondary mr-auto p-2">
            <span class="fa fa-arrow-left" aria-hidden="true"></span> {{vm.generalInfo.voltar.label}}
          </a>
          <button type="button" class="btn btn-sm btn-primary p-2" (click)="vm.mostrarFiltros = !vm.mostrarFiltros">
            <span class="fa fa-filter" aria-hidden="true"></span> {{vm.generalInfo.filtro.label}}
          </button>
          <button type="button" class="btn btn-sm btn-success p-2" (click)="onCreate()">
            <span class="fa fa-plus" aria-hidden="true"></span> {{vm.generalInfo.novoItem.label}}
          </button>
        </div>
      </div>
    </div>
  </div>
  <div class="container-fluid">
    <div class="row">
      <div class="col">
        <h2 class="title--main">
          <span class="fa fa-home" aria-hidden="true"></span> {{ vm.actionTitle }} <small>{{ vm.actionDescription }}</small>
        </h2>
      </div>
    </div>
  </div>
</div>

<div class="container-fluid">
  <div class="row" *ngIf="vm.mostrarFiltros">
    <div class="col">
      <div class="gc-filter">
        <form #formFilter="ngForm" (ngSubmit)="onFilter(vm.modelFilter)">
          <div class="card gc-card card-primary">
            <div class="card-header">{{vm.generalInfo.filtro.label}}</div>
            <div class="card-block">
              <app-<#classNameLowerAndSeparator#>-container-filter [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-filter>
            </div>
            <div class="card-footer d-flex justify-content-end">
              <button class="btn btn-success" type="submit">
                <span class="fa fa-search"></span>
                {{vm.generalInfo.filtrar.label}}
              </button>
			  <button class="btn btn-primary" type="button" (click)="vm.mostrarFiltros =! vm.mostrarFiltros">Fechar</button>
			  <button class="btn btn-default" type="button" (click)="onClearFilter()">Limpar</button>
            </div>
          </div>
        </form>
      </div>
    </div>
  </div>

  <div class="row">
    <div class="col">
      <div class="card gc-grid">
        <div class="card-header gc-grid__header">
		</div>
        <div class="card-block gc-grid__body">
			<make-grid [(vm)]="vm" (edit)="onEdit($event)" (details)="onDetails($event)" (print)="onPrint($event)" (deleteConfimation)="onDeleteConfimation($event)" (orderBy)="onOrderBy($event)"></make-grid>
        </div>
        <div class="card-footer gc-grid__footer">
          <div class="gc-pagination">
            <make-pagination [(vm)]="vm" (pageChanged)="onPageChanged($event)"></make-pagination>
          </div>
        </div>
      </div>
    </div>
  </div>
</div>

<div bsModal #saveModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
        <div class="modal-body">
          <app-<#classNameLowerAndSeparator#>-container-create [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-create>
        </div>
        <div class="modal-footer">
          <button class="btn btn-default" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
          <button type="submit" class="btn btn-success" [disabled]="vm != null && vm.form.invalid" >{{vm.generalInfo.salvar.label}}</button>
		  <section class="col-md-4">
            <div class='checkbox'>
              <label>
                <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' /> Manter Janela Aberta?
              </label>
            </div>
          </section>
        </div>
      </form>
    </div>
  </div>
</div>

<div bsModal #editModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">Manutenção de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <form (ngSubmit)="onSave(vm.model)" novalidate>
		<div class="modal-body">
			<app-<#classNameLowerAndSeparator#>-container-edit [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-edit>
		</div>
        <div class="modal-footer">
          <button class="btn btn-default" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
          <button type="submit" class="btn btn-success" [disabled]="vm != null && vm.form.invalid">{{vm.generalInfo.salvar.label}}</button>
		  <section class="col-md-4">
            <div class='checkbox'>
              <label>
                <input type='checkbox' [(ngModel)]='vm.manterTelaAberta' name='manterTelaAberta' /> Manter Janela Aberta?
              </label>
            </div>
          </section>
        </div>
      </form>
    </div>
  </div>
</div>

<div bsModal #detailsModal="bs-modal" class="gc-modal modal fade">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <div class="modal-header">
        <h4 class="modal-title pull-left">detalhes de {{ vm.actionTitle }}</h4>
        <button type="button" class="close pull-right" aria-label="Close" (click)="onCancel()">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
	  <div class="modal-body">
		<app-<#classNameLowerAndSeparator#>-container-details [(vm)]="vm"></app-<#classNameLowerAndSeparator#>-container-details>
	  </div>
      <div class="modal-footer">
        <button class="btn btn-default" type="button" (click)="onCancel()">{{vm.generalInfo.cancelar.label}}</button>
      </div>
    </div>
  </div>
</div>

