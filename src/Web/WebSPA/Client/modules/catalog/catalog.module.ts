import { NgModule }             from '@angular/core';
import { BrowserModule  }       from '@angular/platform-browser';
import { CommonModule }         from '@angular/common'
import { SharedModule }         from '../shared/shared.module';
import { CatalogComponent }     from './catalog.component';
import { CatalogService }       from './catalog.service';
import { Pager }                from '../shared/components/pager/pager';
import { CatalogDetailComponent } from './catalog-detail/catalog-detail.component';

@NgModule({
    imports: [BrowserModule, SharedModule, CommonModule],
    declarations: [CatalogComponent, CatalogDetailComponent],
    providers: [CatalogService]
})
export class CatalogModule { }
