import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { PagingHeaderComponent } from './paging-header/paging-header.component';
import { PagerComponent } from './pager/pager.component';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { SummaryComponent } from './summary/summary.component';

@NgModule({
  declarations: [
    PagingHeaderComponent,
    PagerComponent,
    SummaryComponent
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    CarouselModule
  ],
  exports: [
    PaginationModule,
    PagingHeaderComponent,
    PagerComponent,
    CarouselModule,
    SummaryComponent
  ]
})
export class SharedModule { }
