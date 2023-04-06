import {Component, OnInit} from '@angular/core';
import {PrimeNGConfig} from "primeng/api";

@Component({
  selector: 'app-access',
  templateUrl: './access.component.html',
  styleUrls: ['./access.component.scss']
})
export class AccessComponent implements OnInit {

  constructor( private primengConfig: PrimeNGConfig ) {
  }
  ngOnInit(): void {
    this.primengConfig.ripple = true;
  }

}
