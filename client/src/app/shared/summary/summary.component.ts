import { Component } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';

@Component({
  selector: 'app-summary',
  templateUrl: './summary.component.html',
  styleUrls: ['./summary.component.scss']
})
export class SummaryComponent {
  constructor(public basketService: BasketService) { }

}
